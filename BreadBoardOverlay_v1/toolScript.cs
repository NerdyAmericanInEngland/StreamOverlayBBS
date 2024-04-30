using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using OpenMacroBoard.SDK;
using StreamDeckSharp;
using ImageNonSharp = System.Drawing.Image;
using KeyEventArgs = OpenMacroBoard.SDK.KeyEventArgs;
using Rectangle = System.Drawing.Rectangle;
using System.Threading.Tasks;
using Brush = System.Drawing.Brush;
using FontFamily = System.Drawing.FontFamily;
using SizeF = System.Drawing.SizeF;

namespace BreadBoardOverlay_v1
{
    public static class toolScript
    {

        public static bool breakEarly;
        public static PictureBox _topLeft;
        public static int currentPage;
        public static int pageButtonNumberForward = 99999999;
        public static int pageButtonNumberReverse = 99999999;
        public static bool hold;
        public static List<int> _StreamDeckButtonsPushed = new List<int>();
        private static int updateIndex;
        private static string rightImage = @"E:\CSharp\bbsOverlay\BreadBoardOverlay_v1\rightarrow.png";
        private static string leftImage = @"E:\CSharp\bbsOverlay\BreadBoardOverlay_v1\leftarrow.png";
        public static FrameDimension dimension;
        public static int frameCount;
        public static int currentFrameNew;
        public static bool imageUpdateLoopRunning;
        private static Mutex mut = new Mutex();
        internal static bool playbackRunning;
        
        public static void playTopLeftGifNonElgato(MembersData _membersData, Button bt)
        {
            try
            {
                if (_membersData.imageFilePath == String.Empty && _membersData.audiofilepath == String.Empty)
                {
                    return;
                }

                if (_topLeft.Enabled && _topLeft.Visible && _membersData.gifPlaying)
                {
                    return;
                }
                foreach (var allMBMR in ControllerWindow.AllMembers)
                {
                    if (allMBMR.gifPlaying && _membersData != allMBMR)
                    {
                        allMBMR.gifPlaying = false;
                        //allMBMR.skipAnimation = true;
                    }
                    
                }
                _topLeft.Image = null;
                var nullorEmptyCheck = !string.IsNullOrEmpty(_membersData.imageFilePath);
                if (nullorEmptyCheck && !_membersData.gifPlaying)
                {
                    _membersData.gifPlaying = true;
                    if (_membersData.gifImage == null)
                    {
                        _membersData.gifImage = Image.FromFile(_membersData.imageFilePath);    
                    }

                    if (_membersData.dimension == null)
                    {
                        dimension = new FrameDimension(_membersData.gifImage.FrameDimensionsList[0]);    
                    }
                    
                    frameCount = _membersData.gifImage.GetFrameCount(dimension);
                    if (_membersData.gifPlaybackTime != 1)
                    {
                        frameCount *= _membersData.gifPlaybackTime;
                    }
                    stopAudio();
                    playGif(_membersData);
                }

                
                if (!_membersData.audioPlaying && _membersData.audiofilepath != null && !string.IsNullOrEmpty(_membersData.audiofilepath))
                {
                    playFixedSound(_membersData);
                    _membersData.audioPlaying = true;
                }

                bt.Enabled = true;
                //_topLeft.SizeMode =PictureBoxSizeMode.StretchImage;
            }
            catch (Exception e)
            {
                loggerErr(e.ToString());
            }
        }
        
        public static void playTopLeftGif(MembersData _membersData)
        {
            try
            {
                if (_membersData == null)
                {
                    loggerErr($"_membersdata value sent was nul, returning");
                    return;
                }

                if (_membersData.imageFilePath == String.Empty && _membersData.audiofilepath == String.Empty)
                {
                    return;
                }

                if (playbackRunning)
                {
                    return;
                }
                
                foreach (var mbmr in ControllerWindow.AllMembers)
                {
                    if (mbmr.gifPlaying)
                    {
                        mbmr.gifPlaying = false;
                        mbmr.audioPlaying = false;
                    }
                }

                _topLeft.Image = null;
                var nullorEmptyCheck = !string.IsNullOrEmpty(_membersData.imageFilePath);
                if (nullorEmptyCheck && !_membersData.gifPlaying)
                {
                    playbackRunning = true;
                    _membersData.gifPlaying = true;
                    if (_membersData.gifImage == null)
                    {
                        _membersData.gifImage = Image.FromFile(_membersData.imageFilePath);    
                    }
                    
                    dimension = new FrameDimension(_membersData.gifImage.FrameDimensionsList[0]);
                    frameCount = _membersData.gifImage.GetFrameCount(dimension);
                    if (_membersData.gifPlaybackTime != 1)
                    {
                        frameCount *= _membersData.gifPlaybackTime;
                    }
                    // int totalLength = 0;
                    stopAudio();
                    playGif(_membersData);
                    
                    
                }
                
                if (!_membersData.audioPlaying && _membersData.audiofilepath != null && !string.IsNullOrEmpty(_membersData.audiofilepath))
                {
                    playFixedSound(_membersData);
                    _membersData.audioPlaying = true;
                }
                
                //_topLeft.SizeMode =PictureBoxSizeMode.StretchImage;
            }
            catch (Exception e)
            {
                playbackRunning = false;
                frameCount = 0;
                currentFrameNew = 0;
                loggerErr(e.ToString());
            }
        }


        public static void playGif(MembersData _membersData)
        {
            try
            {
                if (_membersData.gifImage == null)
                {
                    _membersData.gifImage = Image.FromFile(_membersData.imageFilePath);
                }

                
                _topLeft.Image = _membersData.gifImage;
                _topLeft.Enabled = true;
                _topLeft.Visible = true;
                _membersData.gifPlaying = true;
                ImageAnimator.Animate(_membersData.gifImage, OnFrameChanged);
                Thread nt1 = new Thread(gifLoopCheck);
                nt1.Start();
                _topLeft.SizeMode =PictureBoxSizeMode.StretchImage;
            }
            catch (Exception e)
            {
                _topLeft.Enabled = false;
                _topLeft.Visible = false;
                currentFrameNew = 0;
                frameCount = 0;
                loggerErr(e.ToString());
            }
        }

        


        private static void OnFrameChanged(object sender, EventArgs args)
        {
            try
            {
                bool RunningGif = false;
                foreach (var MBR in ControllerWindow.AllMembers)
                {
                    if (MBR.gifPlaying)
                    {
                        RunningGif = true;
                    }
                }

                if (RunningGif)
                {
                    currentFrameNew++;
                }
            }
            catch (Exception e)
            {
                loggerErr(e.ToString());
            }
        }

        public static void setPlaybackSpeedBoxchange(TextBox textBoxChanged)
        {
            int tag = Int32.Parse(textBoxChanged.Tag.ToString());
            int speed = Int32.Parse(textBoxChanged.Text);
            ControllerWindow.AllMembers[tag].gifPlaybackTime = speed;
            frameCount = 0;
            dimension = new FrameDimension(ControllerWindow.AllMembers[tag].gifImage.FrameDimensionsList[0]);
            frameCount = ControllerWindow.AllMembers[tag].gifImage.GetFrameCount(dimension);
            frameCount *= speed;
            Console.WriteLine($"set frame count to {frameCount}");
        }

        public static void mg(string messagee)
        {
            Console.WriteLine(messagee);
            
        }

        public static void stopGif(ImageNonSharp gifImage = null)
        {
            try
            {
                breakEarly = true;
                _topLeft.Image = null;
                foreach (var member in ControllerWindow.AllMembers)
                {
                    playbackRunning = false;
                    member.audioPlaying = false;
                    member.gifPlaying = false;
                    if (member.audiofilepath != null && member.audiofilepath != "")
                    {
                        SoundPlayer player = new SoundPlayer(member.audiofilepath);
                        player.Stop();
                    }
                    //currentFrame = 2000000;
                }
            }
            catch (Exception e)
            {
                loggerErr(e.ToString());
            }
        }
        
        public static void stopAudio()
        {
            try
            {
                foreach (var member in ControllerWindow.AllMembers)
                {
                    if (member.audioPlaying && member.audiofilepath != null && member.audiofilepath != "")
                    {
                        SoundPlayer player = new SoundPlayer(member.audiofilepath);
                        player.Stop();
                        member.audioPlaying = false;
                    }
                }
            }
            catch (Exception e)
            {
                loggerErr(e.ToString());
            }
        }

        public static void playFixedSound(MembersData _membersData)
        {
            try
            {
                SoundPlayer player = new SoundPlayer(_membersData.audiofilepath);
                player.Play();
            }
            catch (Exception e)
            {
                loggerErr(e.ToString());
            }
        }

        #region Video Related

        // public static void PlayVideo()
        // {
        //     // MediaPlayer mPlayer = MediaPlayer.create(this, R.raw.baitikochi_chuste);
        //     MediaPlayer player = new MediaPlayer();
        //     player.Open(new Uri(@"E:\Movies\A_CHARLIE_BROWN_CHRISTMAS\A_CHARLIE_BROWN_CHRISTMAS.mp4", UriKind.Relative));
        //     VideoDrawing aVideoDrawing = new VideoDrawing();
        //     aVideoDrawing.Rect = new Rect(0, 0, 100, 100);
        //     aVideoDrawing.Rect = new Rect(0, 0, 100, 100);
        // }
        
       

        #endregion

        public static void mapNavButtonToImage(int buttonNumber, bool rightButton = false)
        {
            try
            {
                    //                if (!buttonCheckRunning)
                //{
                    //getKeysPressedStreamDeck();    
                //}
                if (rightButton)
                {
                    if (_StreamDeckButtonsPushed.Count > 0)
                    {
                        getImageForButton(rightImage,_StreamDeckButtonsPushed[0]);
                        pageButtonNumberForward = _StreamDeckButtonsPushed[0];
                        //ControllerWindow.AllMembers[memberIndex].deckButtonNumber = _StreamDeckButtonsPushed[0];
                        return;
                    }
                }
                else
                {
                    if (_StreamDeckButtonsPushed.Count > 0)
                    {
                        pageButtonNumberReverse = _StreamDeckButtonsPushed[0];
                        getImageForButton(leftImage,_StreamDeckButtonsPushed[0]);
                        return;
                    }
                }
                //Thread.Sleep(1200);
                Task.Delay(1200).ContinueWith(t=> mapNavButtonToImage(buttonNumber,rightButton));
                //mapNavButtonToImage(buttonNumber,rightButton);
                
            }
            catch (Exception e)
            {
                loggerErr(e.ToString());
            }
        }

        public static void getKeysPressedStreamDeck()
        {
            try
            {
                var deck = StreamDeck.OpenDevice();
                deck.KeyStateChanged += Deck_KeyPressed;
            }
            catch (Exception e)
            {
                loggerErr(e.ToString());
            }
        }


        public static void getImageForButton(string FilePath,int buttonNumber)
        {
            try
            {
                IMacroBoard deck = StreamDeck.OpenDevice();
                KeyBitmap buttonImage = KeyBitmap.Create.FromFile(FilePath);
                deck.SetKeyBitmap(buttonNumber,buttonImage);
            }
            catch (Exception e)
            {
                loggerErr(e.ToString());
            }
        }
        public static void getImageForAllButtons()
        {
            try
            {
                //IMacroBoard deck = StreamDeck.OpenDevice();
                mg("Updating elgato buttons");
                foreach (var member in ControllerWindow.AllMembers.ToList())
                {
                    if (member.deckButtonNumber == -1)
                    {
                        continue;
                    }
                    //Console.WriteLine("members button:" + member.deckButtonNumber);
                    
                    if (member.imageFilePath != null && member.streamDeckpageNumber == currentPage && member.deckButtonNumber != -1)
                    {
                        getImageForButton(member.imageFilePath,member.deckButtonNumber);
                    }
                
                }

                if (pageButtonNumberForward != 99999999)
                {
                    getImageForButton(rightImage,pageButtonNumberForward);
                }
                if (pageButtonNumberReverse != 99999999)
                {
                    getImageForButton(leftImage,pageButtonNumberReverse);
                }
            }
            catch (Exception e)
            {
                loggerErr(e.ToString());
            }
        }
        

        public static void setButtonToBlack(int buttonNumber)
        {
            try
            {
                IMacroBoard deck = StreamDeck.OpenDevice();
                KeyBitmap black = KeyBitmap.Create.FromRgb(0,0,0);
                deck.SetKeyBitmap(buttonNumber,black);
            }
            catch (Exception e)
            {
                loggerErr(e.ToString());
            }
        }

        public static void Deck_KeyPressed(object sender, KeyEventArgs e)
        {
            try
            {
                if (hold)
                {
                    return;
                }
                if (e.IsDown)
                {
                    if (!_StreamDeckButtonsPushed.Contains(e.Key) && _StreamDeckButtonsPushed.Count == 0)
                    {
                        _StreamDeckButtonsPushed.Add(e.Key);
                    }
                    else
                    {
                        return;
                    }
                    
                    
                    foreach (var mbmr in ControllerWindow.AllMembers)
                    {
                        if (!mbmr.gifPlaying && mbmr.deckButtonNumber == e.Key && mbmr.streamDeckpageNumber == currentPage)
                        {
//                            mut.ReleaseMutex();
                            mut.WaitOne();
                            playTopLeftGif(mbmr);
                            mut.ReleaseMutex();
                            
                        }
                    }
                    if (pageButtonNumberForward == e.Key)
                    {
                        clearAllNonAssigned();
                        setNextPageImages();
                    
                    }
                    if (pageButtonNumberReverse == e.Key)
                    {
                        if (currentPage == 0)
                        {
                            return;
                        }
                        clearAllNonAssigned();
                        setPreviousPageImages();
                    }
                    return;
                }
                if (_StreamDeckButtonsPushed.Count == 0 && _StreamDeckButtonsPushed.Contains(e.Key))
                {
                    _StreamDeckButtonsPushed.Remove(e.Key);
                }
                if (_StreamDeckButtonsPushed.Contains(e.Key))
                {
                    _StreamDeckButtonsPushed.Remove(e.Key);
                }
            }
            catch (Exception exception)
            {
                //mut.ReleaseMutex();
                loggerErr(exception.ToString());
            }
        }
        
        
        private static void clearAllNonAssigned()
        {
            try
            {
                foreach (var mbr in ControllerWindow.AllMembers)
                {
                    if (currentPage == mbr.streamDeckpageNumber)
                    {
                        setButtonToBlack(mbr.deckButtonNumber);
                    }
                }
            }
            catch (Exception e)
            {
                loggerErr(e.ToString());
            }
        }
        

        private static void setNextPageImages()
        {
            try
            {
                mg("setNextPageImages run");
                foreach (var mbd in ControllerWindow.AllMembers)
                {
                    if (mbd.streamDeckpageNumber == currentPage)
                    {
                        setButtonToBlack(mbd.deckButtonNumber);
                    }
                }
                currentPage++;
                getImageForAllButtons();
            }
            catch (Exception e)
            {
                loggerErr(e.ToString());
            }
            
        }
        private static void setPreviousPageImages()
        {
            try
            {
                mg("setPreviousPageImages Run");
                if (currentPage <= 0)
                {
                    return;
                }
                foreach (var mbd in ControllerWindow.AllMembers)
                {
                    if (mbd.streamDeckpageNumber == currentPage)
                    {
                        setButtonToBlack(mbd.deckButtonNumber);
                    }
                }
                currentPage--;
                getImageForAllButtons();
            }
            catch (Exception e)
            {
                loggerErr(e.ToString());
            }
            
        }

        public static void loggerErr(string error, bool writeToFile = false)
        {
            Console.WriteLine($"error is: {error}");
        }

        

        public static void gifLoopCheck()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(20);
                    if (currentFrameNew >= frameCount)
                    {
                        Console.WriteLine($"Stopping, currentFrameNew {currentFrameNew} frameCount {frameCount}");
                        _topLeft.Visible = false;
                        _topLeft.Enabled = false;
                        foreach (var mbmr in ControllerWindow.AllMembers)
                        {
                            if (mbmr.gifPlaying)
                            {
                                mbmr.gifPlaying = false;
                                ImageAnimator.StopAnimate(mbmr.gifImage,OnFrameChanged);
                                if (mbmr.audioPlaying)
                                {
                                    SoundPlayer player = new SoundPlayer(mbmr.audiofilepath);
                                    player.Stop();
                                    mbmr.audioPlaying = false;
                                }
                            }
                        }
                        playbackRunning = false;
                        currentFrameNew = 0;
                        frameCount = 0;
                        breakEarly = true;
                        break;
                    }
                
                }
            }
            catch (Exception e)
            {
                playbackRunning = false;
                foreach (var mbr in ControllerWindow.AllMembers)
                {
                    if (mbr.gifPlaying)
                    {
                        mbr.gifPlaying = false;
                    }
                }
                currentFrameNew = 0;
                frameCount = 0;
                loggerErr("exception" + e.ToString());
            }
            
        }
        

        // public static void updateLoop()
        // {
        //     try
        //     {
        //         if (hold)
        //         {
        //             Task.Delay(900).ContinueWith(t => updateLoop());
        //             return;
        //         }
        //
        //         if (imageUpdateLoopRunning)
        //         {
        //             Console.WriteLine("returning");
        //             Task.Delay(900).ContinueWith(t => updateLoop());
        //             return;
        //         }
        //
        //         if (updateIndex == 3)
        //         {
        //             getImageForAllButtons();
        //         }
        //
        //         if (updateIndex == 10)
        //         {
        //             updateIndex = 0;
        //         }
        //         else
        //         {
        //             updateIndex++;
        //         }
        //
        //         int countRunning = 0;
        //         foreach (var mbmr in ControllerWindow.AllMembers)
        //         {
        //             if (mbmr.gifPlaying)
        //             {
        //                 countRunning++;
        //             }
        //         }
        //
        //         if (countRunning == 0)
        //         {
        //             currentFrameNew = 0;
        //             frameCount = 0;
        //         }
        //         _topLeft.Image = null;
        //         Task.Delay(900).ContinueWith(t=> updateLoop());
        //     }
        //     catch (Exception e)
        //     {
        //         loggerErr(e.ToString());
        //         Task.Delay(900).ContinueWith(t=> updateLoop());
        //     }
        // }
        
        
        public partial class MembersData
        {
            public string imageFilePath;
            public string audiofilepath;
            public int streamDeckpageNumber;
            public int deckButtonNumber = -1;
            public int tagNumber;
            public int gifPlaybackTime;
            public bool gifPlaying;
            public bool audioPlaying;
            public Label _LabelNameBox;
            public TextBox _testBoxSpeed;
            public Panel panelPlayingAssigned;
            public Image gifImage;
            public FrameDimension dimension;
        }
    }
}