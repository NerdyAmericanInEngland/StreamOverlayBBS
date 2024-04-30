using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ContentAlignment = System.Drawing.ContentAlignment;


namespace BreadBoardOverlay_v1
{
    public partial class ControllerWindow : Form
    {
        private Panel TopLeftPanel;
        private int columnNumber ;
        private Panel _lastpanel;
        public static List<toolScript.MembersData> AllMembers = new List<toolScript.MembersData>();

        public ControllerWindow()
        {
            try
            {
                InitializeComponent();
                AllMembers.Add(new toolScript.MembersData());
                AllMembers[0].tagNumber = 0;
                AllMembers[0]._LabelNameBox = label1;
                AllMembers[0]._testBoxSpeed = textBox1;
                createNewForm();
                //Thread thread1 = new Thread(toolScript.updateLoop);
                //thread1.Start();
                //toolScript.updateLoop();
            }
            catch (Exception e)
            {
                toolScript.loggerErr(e.ToString());
            }

        }

        private void createNewForm()
        {
            MultiFormContext newForm = new MultiFormContext(new mainWindow());
        }

        private void armOverlay(object sender, EventArgs e)
        {
            createNewForm();

        }

        
        
        public void PlayClick(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                btn.Enabled = false;
                btn.Update();
                int tag = Int32.Parse(btn.Tag.ToString());
                toolScript.playTopLeftGifNonElgato(AllMembers[tag],btn);
                Application.DoEvents();
                btn.Enabled = true;
            }
            catch (Exception exception)
            {
                toolScript.loggerErr(e.ToString());
            }
        }

        
        private void addInteractionRemotely(int memberNumber, bool reload = false)
        {
            try
            {
                if (columnNumber < 4||(reload && columnNumber < 5 && memberNumber < 6))
                {
                    if (_lastpanel == null)
                    {
                        _lastpanel = panel1;
                        // if (!toolScript.buttonCheckRunning)
                        // {
                            //toolScript.getKeysPressedStreamDeck();    
                        // }
                        
                    }
                    var _Panel = new Panel()
                    {
                        Location = new Point((panel1.Location.X + 240), panel1.Location.Y), BackColor = Color.White,
                        Size = panel1.Size
                    };
                
                
                    Button buttonPlay = new Button() { Text = "Play/Stop", Location = new Point(button1.Location.X,button1.Location.Y), Size = button1.Size};
                    buttonPlay.BackColor = button1.BackColor;
                    buttonPlay.Click += new EventHandler(PlayClick);
                    buttonPlay.Tag = AllMembers[memberNumber].tagNumber;
                
                    Button buttonSelectGif = new Button() { Text = "Select Gif", Location = new Point(button3.Location.X,button3.Location.Y), Size = button3.Size};
                    buttonSelectGif.BackColor = button1.BackColor;
                    buttonSelectGif.Click += new EventHandler(selectGif);
                    buttonSelectGif.Tag = AllMembers[memberNumber].tagNumber;
                
                
                    Button buttonSelectSound = new Button() { Text = "Select Sound", Location = new Point(button4.Location.X,button4.Location.Y), Size = button4.Size};
                    buttonSelectSound.BackColor = button1.BackColor;
                    buttonSelectSound.Click += new EventHandler(selectWav);
                    buttonSelectSound.Tag = AllMembers[memberNumber].tagNumber;
                
                    Button buttonMapStreamDeckButton = new Button() { Text = "Bind Stream Deck Button", Location = new Point(button6.Location.X,button6.Location.Y), Size = button6.Size};
                    buttonMapStreamDeckButton.BackColor = button1.BackColor;
                    buttonMapStreamDeckButton.Click += new EventHandler(mapButtonToImage);
                    buttonMapStreamDeckButton.Tag = AllMembers.Count - 1;
                
                    Label labelBoxName = new Label() { Location = new Point(label1.Location.X,label1.Location.Y), Size = label1.Size};
                    labelBoxName.Tag = AllMembers[memberNumber].tagNumber;
                    AllMembers[memberNumber]._LabelNameBox = labelBoxName;
                    
                    Label LabelSpeed = new Label() { Location = new Point(label2.Location.X,label2.Location.Y), Size = label2.Size};
                    LabelSpeed.Text = "";
                    LabelSpeed.TextAlign = ContentAlignment.TopCenter;
                    LabelSpeed.Text = "Playback Speed";
                    
                    
                    TextBox TextBoxSpeed = new TextBox() { Location = new Point(textBox1.Location.X,textBox1.Location.Y), Size = textBox1.Size};
                    TextBoxSpeed.Tag = AllMembers[memberNumber].tagNumber;
                    Console.WriteLine($"setting tag to {AllMembers[memberNumber].tagNumber}");
                    AllMembers[memberNumber]._testBoxSpeed = TextBoxSpeed;
                    AllMembers[memberNumber]._testBoxSpeed.Text = AllMembers[memberNumber].gifPlaybackTime.ToString();
                    TextBoxSpeed.TextChanged += new EventHandler(TextChangedGeneric);
                  
                    
                    
                    if (!String.IsNullOrEmpty(AllMembers[memberNumber].imageFilePath))
                    {
                        var fullpath = AllMembers[memberNumber].imageFilePath.Split('\\');
                        if (fullpath.Length != 0)
                        {
                            AllMembers[memberNumber]._LabelNameBox.Text = fullpath[fullpath.Length - 1].Split('.')[0];    
                        }
                    }
                    else if(!String.IsNullOrEmpty(AllMembers[memberNumber].audiofilepath))
                    {
                        var fullpathSound = AllMembers[memberNumber].audiofilepath.Split('\\');
                        if (fullpathSound.Length != 0)
                        {
                            AllMembers[memberNumber]._LabelNameBox.Text = fullpathSound[fullpathSound.Length - 1].Split('.')[0];    
                        }
                    
                    }
                
                
                    //var controlBox = new Control[] { label, textbox, button, _Panel };
                    var controlBox = new Control[] { buttonPlay,buttonSelectGif,buttonSelectSound,labelBoxName,TextBoxSpeed,LabelSpeed, _Panel };
                    ActiveForm.Controls.AddRange(controlBox);
                    _Panel.Controls.Add(buttonPlay);
                    _Panel.Controls.Add(buttonSelectGif);
                    _Panel.Controls.Add(buttonSelectSound);
                    _Panel.Controls.Add(buttonMapStreamDeckButton);
                    _Panel.Controls.Add(labelBoxName);
                    _Panel.Controls.Add(TextBoxSpeed);
                    _Panel.Controls.Add(LabelSpeed);
                    if (reload && memberNumber == 0)
                    {
                        _Panel.Location = new Point(panel1.Location.X, panel1.Location.Y);
                    }
                    else
                    {
                        _Panel.Location = new Point(_lastpanel.Location.X + 200, _lastpanel.Location.Y);
                    }
                    
                    _lastpanel = _Panel;
                    if (reload && memberNumber == 0 && columnNumber == 0)
                    {
                        //_Panel.Location = panel1.Location;
                        panel1.Visible = false;
                        //panel1 = _Panel;
                    }
                    columnNumber++;
                }
                else
                {
                    var _Panel = new Panel()
                    {
                        Location = new Point((panel1.Location.X), panel1.Location.Y), BackColor = Color.White,
                        Size = panel1.Size
                    };
                
                
                    Button buttonPlay = new Button() { Text = "Play/Stop", Location = new Point(button1.Location.X,button1.Location.Y), Size = button1.Size};
                    buttonPlay.BackColor = button1.BackColor;
                    buttonPlay.Click += new EventHandler(PlayClick);
                    buttonPlay.Tag = AllMembers[memberNumber].tagNumber;
                
                    Button buttonSelectGif = new Button() { Text = "Select Gif", Location = new Point(button3.Location.X,button3.Location.Y), Size = button3.Size};
                    buttonSelectGif.BackColor = button1.BackColor;
                    buttonSelectGif.Click += new EventHandler(selectGif);
                    buttonSelectGif.Tag = AllMembers[memberNumber].tagNumber;
                
                
                    Button buttonSelectSound = new Button() { Text = "Select Sound", Location = new Point(button4.Location.X,button4.Location.Y), Size = button4.Size};
                    buttonSelectSound.BackColor = button1.BackColor;
                    buttonSelectSound.Click += new EventHandler(selectWav);
                    buttonSelectSound.Tag = AllMembers[memberNumber].tagNumber;
                
                    Button buttonMapStreamDeckButton = new Button() { Text = "Bind Stream Deck Button", Location = new Point(button6.Location.X,button6.Location.Y), Size = button6.Size};
                    buttonMapStreamDeckButton.BackColor = button1.BackColor;
                    buttonMapStreamDeckButton.Click += new EventHandler(mapButtonToImage);
                    buttonMapStreamDeckButton.Tag = AllMembers.Count - 1;
                
                    Label labelBoxName = new Label() { Location = new Point(label1.Location.X,label1.Location.Y), Size = label1.Size};
                    label1.Tag = AllMembers[memberNumber].tagNumber;
                    //textBoxName.Text = AllMembers[memberNumber].imageFilePath.Split('\\')[];
                    
                    Label LabelSpeed = new Label() { Location = new Point(label2.Location.X,label2.Location.Y), Size = label2.Size};
                    LabelSpeed.Text = "";
                    LabelSpeed.TextAlign = ContentAlignment.TopCenter;
                    LabelSpeed.Text = "Playback Speed";
                    
                    TextBox TextBoxSpeed = new TextBox() { Location = new Point(textBox1.Location.X,textBox1.Location.Y), Size = textBox1.Size};
                    TextBoxSpeed.Tag = AllMembers[memberNumber].tagNumber;
                    AllMembers[memberNumber]._testBoxSpeed = TextBoxSpeed;
                    AllMembers[memberNumber]._testBoxSpeed.Text = AllMembers[memberNumber].gifPlaybackTime.ToString();
                    TextBoxSpeed.TextChanged += new EventHandler(TextChangedGeneric);
                
                    if (!String.IsNullOrEmpty(AllMembers[memberNumber].imageFilePath))
                    {
                        var fullpath = AllMembers[memberNumber].imageFilePath.Split('\\');
                        if (fullpath.Length != 0)
                        {
                            labelBoxName.Text = fullpath[fullpath.Length - 1].Split('.')[0];    
                        }
                    }
                    else if(!String.IsNullOrEmpty(AllMembers[memberNumber].audiofilepath))
                    {
                        var fullpathSound = AllMembers[memberNumber].audiofilepath.Split('\\');
                        if (fullpathSound.Length != 0)
                        {
                            labelBoxName.Text = fullpathSound[fullpathSound.Length - 1].Split('.')[0];    
                        }
                    
                    }
                
                    var controlBox = new Control[] { buttonPlay,buttonSelectGif,buttonSelectSound,labelBoxName,TextBoxSpeed,LabelSpeed,_Panel };
                    ActiveForm.Controls.AddRange(controlBox);
                    _Panel.Controls.Add(buttonPlay);
                    _Panel.Controls.Add(buttonSelectGif);
                    _Panel.Controls.Add(buttonSelectSound);
                    _Panel.Controls.Add(buttonMapStreamDeckButton);
                    _Panel.Controls.Add(labelBoxName);
                    _Panel.Controls.Add(TextBoxSpeed);
                    _Panel.Controls.Add(LabelSpeed);
                    _Panel.Location = new Point(panel1.Location.X, _lastpanel.Location.Y + 300);
                    _lastpanel = _Panel;
                    columnNumber = 0;
                    // if (reload)
                    // {
                    //     _Panel.Location = panel1.Location;
                    // }
                }
            }
            catch (Exception e)
            {
                toolScript.loggerErr(e.ToString());
            }
            
        }
        

        private void addInteraction(object sender, EventArgs e)
        {
            try
            {
                AllMembers.Add(new toolScript.MembersData());
                Console.WriteLine($"allmembers length is {AllMembers.Count}");
                AllMembers[AllMembers.Count - 1].tagNumber = AllMembers.Count - 1;
                if (_lastpanel == null)
                {
                    _lastpanel = panel1;
                }

                if (columnNumber < 4)
                {
                    columnNumber++;
                }
                else
                {
                    columnNumber = 0;
                }
                var _Panel = new Panel()
                {
                    Location = new Point((panel1.Location.X + 240), panel1.Location.Y), BackColor = Color.White,
                    Size = panel1.Size
                };
                AllMembers[(AllMembers.Count - 1)].panelPlayingAssigned = _Panel;
                Button buttonPlay = new Button() { Text = "Play/Stop", Location = new Point(button1.Location.X,button1.Location.Y), Size = button1.Size};
                buttonPlay.BackColor = button1.BackColor;
                buttonPlay.Click += new EventHandler(PlayClick);
                buttonPlay.Tag = AllMembers.Count - 1;
            
                Button buttonSelectGif = new Button() { Text = "Select Gif", Location = new Point(button3.Location.X,button3.Location.Y), Size = button3.Size};
                buttonSelectGif.BackColor = button1.BackColor;
                buttonSelectGif.Click += new EventHandler(selectGif);
                buttonSelectGif.Tag = AllMembers.Count - 1;
            
            
                Button buttonSelectSound = new Button() { Text = "Select Sound", Location = new Point(button4.Location.X,button4.Location.Y), Size = button4.Size};
                buttonSelectSound.BackColor = button1.BackColor;
                buttonSelectSound.Click += new EventHandler(selectWav);
                buttonSelectSound.Tag = AllMembers.Count - 1;
            
                Button buttonMapStreamDeckButton = new Button() { Text = "Bind Stream Deck Button", Location = new Point(button6.Location.X,button6.Location.Y), Size = button6.Size};
                buttonMapStreamDeckButton.BackColor = button1.BackColor;
                buttonMapStreamDeckButton.Click += new EventHandler(mapButtonToImage);
                buttonMapStreamDeckButton.Tag = AllMembers.Count - 1;
            
                Label labelBoxName = new Label() { Location = new Point(label1.Location.X,label1.Location.Y), Size = label1.Size};
                labelBoxName.Tag = AllMembers.Count - 1;
                AllMembers[AllMembers.Count - 1]._LabelNameBox = labelBoxName;
                
                Label LabelSpeed = new Label() { Location = new Point(label2.Location.X,label2.Location.Y), Size = label2.Size};
                LabelSpeed.Text = "";
                LabelSpeed.TextAlign = ContentAlignment.TopCenter;
                LabelSpeed.Text = "Playback Length";
                
                TextBox TextBoxSpeed = new TextBox() { Location = new Point(textBox1.Location.X,textBox1.Location.Y), Size = textBox1.Size};
                TextBoxSpeed.Tag = AllMembers.Count - 1;
                TextBoxSpeed.Text = "1";
                TextBoxSpeed.TextChanged += new EventHandler(TextChangedGeneric);
                AllMembers[AllMembers.Count - 1]._testBoxSpeed = TextBoxSpeed;
                AllMembers[AllMembers.Count - 1].gifPlaybackTime = 1;
                var controlBox = new Control[] { buttonPlay,buttonSelectGif,buttonSelectSound,buttonMapStreamDeckButton, labelBoxName, TextBoxSpeed, LabelSpeed, _Panel };
            
            
                ActiveForm.Controls.AddRange(controlBox);
                _Panel.Controls.Add(buttonPlay);
                _Panel.Controls.Add(buttonSelectGif);
                _Panel.Controls.Add(buttonSelectSound);
                _Panel.Controls.Add(buttonMapStreamDeckButton);
                _Panel.Controls.Add(labelBoxName);
                _Panel.Controls.Add(TextBoxSpeed);
                _Panel.Controls.Add(LabelSpeed);
                if (columnNumber == 0)
                {
                    _Panel.Location = new Point(panel1.Location.X, _lastpanel.Location.Y + 260);
                }
                else
                {
                    _Panel.Location = new Point(_lastpanel.Location.X + 175, _lastpanel.Location.Y);    
                }
            
                _lastpanel = _Panel;
                
            }
            catch (Exception exception)
            {
                toolScript.loggerErr(exception.ToString());
            }
        }

  
        
        public void selectGif(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int tagNumber = 0;
                tagNumber = Int32.Parse(btn.Tag.ToString());
                Label textBox = AllMembers[tagNumber]._LabelNameBox;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = "E:\\CSharp\\bbsOverlay\\gifs\\";
                openFileDialog1.Filter = "gif files (*.gif)|*.gif;";
                openFileDialog1.FilterIndex = 0;
                // openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                string selectedFileName = openFileDialog1.FileName;
                AllMembers[tagNumber].imageFilePath = selectedFileName;
                // AllMembers[tagNumber].indexNumber = tagNumber;
                if (textBox != null)
                {
                    AllMembers[tagNumber]._LabelNameBox.Text = openFileDialog1.SafeFileName.Split('.')[0];
                
                }
                else
                {
                    Console.WriteLine("localtextbox is null");
                }
            }
            catch (Exception exception)
            {
                toolScript.loggerErr(exception.ToString());
            }
        }
        
        
        private void selectWav(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int tag = Int32.Parse(btn.Tag.ToString());

                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = "E:\\csharpCode\\bbsOverlay\\wav\\";
                openFileDialog1.Filter = "wav files (*.wav)|*.wav;";
                openFileDialog1.FilterIndex = 0;
                // openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                string selectedFileName = openFileDialog1.FileName;
                AllMembers[tag].audiofilepath = selectedFileName;
                // AllMembers[tag].indexNumber = tag;
            }
            catch (Exception exception)
            {
                toolScript.loggerErr(exception.ToString());
            }
        }

        private void saveLayout(object sender, EventArgs e)
        {
            try
            {
                
                List<string> fullDump = new List<string>();
                foreach (var _member in AllMembers)
                {
                    string createText = _member.tagNumber + "," + _member.imageFilePath + "," + _member.audiofilepath + "," + _member.deckButtonNumber + "," + _member.streamDeckpageNumber + "," + _member.gifPlaybackTime;
                    fullDump.Add(createText);
                }
                string path = @"config.txt";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                File.WriteAllLines(path, fullDump.ToArray());
            }
            catch (Exception exception)
            {
                toolScript.loggerErr(e.ToString());
            }
        }

        private void loadLayout(object sender, EventArgs e)
        {
            // Reading the contents of the file
            try
            {
                foreach (var mbmr in AllMembers.ToList())
                {
                    if (mbmr.panelPlayingAssigned != null)
                    {
                        mbmr.panelPlayingAssigned.Dispose();    
                    }
                    
                }
                AllMembers.Clear();
                string path = @"config.txt";
                string [] allLines = File.ReadAllLines(path);
                int _indexNumber = 0;
                foreach (var _lines in allLines)
                {
                    AllMembers.Add(new toolScript.MembersData());
                    if (_lines == String.Empty||_lines == "")
                    {
                        _indexNumber++;
                        continue;
                    }
                    var splitLine = _lines.Split(',');
                    AllMembers[_indexNumber].tagNumber = Int32.Parse(splitLine[0]);
                
                    if (splitLine.Length > 1 && splitLine[1] != null && splitLine[1] != String.Empty)
                    {
                        AllMembers[_indexNumber].imageFilePath = splitLine[1];
                    }
                
                    if (splitLine.Length > 2)
                    {
                        AllMembers[_indexNumber].audiofilepath = splitLine[2];
                    }
                
                    if (splitLine.Length > 3)
                    {
                        AllMembers[_indexNumber].deckButtonNumber = Int32.Parse(splitLine[3]);
                    }
                
                    if (splitLine.Length > 4)
                    {
                        AllMembers[_indexNumber].streamDeckpageNumber = Int32.Parse(splitLine[4]);
                    }
                    
                    if (splitLine.Length > 5)
                    {
                        AllMembers[_indexNumber].gifPlaybackTime = Int32.Parse(splitLine[5]);
                        Console.WriteLine($"loaded and set value to {AllMembers[_indexNumber].gifPlaybackTime}");
                    }

                    addInteractionRemotely(_indexNumber,true);
                    _indexNumber++;
                }
                toolScript.getImageForAllButtons();
                createNewForm();
            }
            catch (Exception exception)
            {
                toolScript.loggerErr(exception.ToString());
            }
        }

        

        public void mapButtonToImage(object sender, EventArgs e)
        {
            try
            {
                // if (!toolScript.buttonCheckRunning)
                // {
                    //toolScript.getKeysPressedStreamDeck();    
                //}
                
                Button btn = (Button)sender;
                int tag = Int32.Parse(btn.Tag.ToString());
                if (toolScript._StreamDeckButtonsPushed.Count > 0)
                {
                    foreach (var mbr in AllMembers)
                    {
                        if (mbr.deckButtonNumber == toolScript._StreamDeckButtonsPushed[0] &&
                            mbr.streamDeckpageNumber == toolScript.currentPage)
                        {
                            mbr.deckButtonNumber = -1;
                        }
                    }
                    AllMembers[tag].deckButtonNumber = toolScript._StreamDeckButtonsPushed[0];
                    AllMembers[tag].streamDeckpageNumber = toolScript.currentPage;
                    toolScript.getImageForButton(AllMembers[tag].imageFilePath,toolScript._StreamDeckButtonsPushed[0]);
                }
                else
                {
                    Thread.Sleep(1200);
                    mapButtonToImage(sender,e);
                }
            }
            catch (Exception exception)
            {
                toolScript.loggerErr(exception.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                toolScript.mapNavButtonToImage(5);
            }
            catch (Exception exception)
            {
                toolScript.loggerErr(exception.ToString());
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            try
            {
                toolScript.mapNavButtonToImage(7,true);
            }
            catch (Exception exception)
            {
                toolScript.loggerErr(exception.ToString());
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (toolScript.hold)
                {
                    toolScript.hold = false;
                    //toolScript.updateLoop();
                }
                else
                {
                    toolScript.hold = true;
                }
            }
            catch (Exception exception)
            {
                toolScript.loggerErr(exception.ToString());
            }
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                toolScript.stopGif();
            }
            catch (Exception exception)
            {
                toolScript.loggerErr(exception.ToString());
            }
        }


        private void TextChangedGeneric(object sender, EventArgs e)
        {
            try
            {
                toolScript.mg("set playback speed");
                TextBox textSender = (TextBox)sender;
                if (textSender.Text == "")
                {
                    toolScript.loggerErr($"returning, textsender.text is blanmk");
                    return;
                }
                toolScript.setPlaybackSpeedBoxchange(textSender);
            }
            catch (Exception exception)
            {
                return;
            }
        }

        private void ControllerWindow_Load(object sender, EventArgs e)
        {
            toolScript.getKeysPressedStreamDeck(); 
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"current value is {AllMembers[0].gifPlaybackTime}");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Console.WriteLine(toolScript.currentFrameNew + " is the current frame count");
        }
    }

    public class MultiFormContext : ApplicationContext
    {
        private int openForms;
        public MultiFormContext(params Form[] forms)
        {
            openForms = forms.Length;

            foreach (var form in forms)
            {
                form.FormClosed += (s, args) =>
                {
                    //When we have closed the last of the "starting" forms, 
                    //end the program.
                    if (Interlocked.Decrement(ref openForms) == 0)
                        ExitThread();
                };

                form.Show();
            }
        }
    }

}