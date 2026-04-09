#nullable disable
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RestaurantDesktopApp
{
    public class LandingForm : Form
    {
        private Panel headerPanel;
        private Panel mainScrollPanel;
        private FlowLayoutPanel navFlow;

        // Section panels (kept as fields for resize)
        private Panel heroPanel;
        private Panel aboutPanel;
        private Panel statsPanel;
        private Panel dishesPanel;
        private Panel footerPanel;
        private Panel contactPanel;
        private Panel servicePanel;

        // Service / menu
        private FlowLayoutPanel _svcGrid;
        private Button _svcCartBtn;
        private int    _cartCount = 0;
        private List<CartItem> _cartItems = new List<CartItem>();
        private Panel _cartOverlay;
        private Panel _paymentOverlay;

        public class CartItem
        {
            public string Name { get; set; }
            public int Price { get; set; }
            public int Quantity { get; set; }
        }

        // Hero internal controls (resized dynamically)
        private PictureBox buildingPic;
        private Label lblTitle1, lblTitle2, lblTitle3, lblDesc;
        private Button orderBtn;

        // Dishes internal
        private Panel[] dishCards = new Panel[3];
        private Button moreBtn;
        private Label dishSectionTitle;
        private Panel dishUnderline;

        // Stats internal
        private Panel[] statCards = new Panel[3];
        private Label statsTitle, statsDesc;

        // Footer
        private Label copyRight;

        // Colors
        private Color darkBg       = Color.FromArgb(15, 23, 42);
        private Color lightBeige   = Color.FromArgb(253, 246, 234);
        private Color accentOrange = Color.FromArgb(250, 163, 7);
        private Color textDark     = Color.FromArgb(17, 24, 39);
        private Color textLight    = Color.FromArgb(243, 244, 246);

        // Section heights (fixed)
        private const int HERO_H   = 600;
        private const int ABOUT_H  = 800;
        private const int STATS_H  = 300;
        private const int DISHES_H = 560;
        private const int FOOTER_H = 400;
        private const int CONTACT_H = 680;

        public LandingForm()
        {
            InitializeComponent();
            SetupUI();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            ClientSize = new Size(1284, 760);
            MinimumSize = new Size(900, 600);
            Name = "LandingForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DBU LAUNCH";
            Load += LandingForm_Load;
            Resize += LandingForm_Resize;
            ResumeLayout(false);
        }

        private void SetupUI()
        {
            this.BackColor = darkBg;

            // ── Header ─────────────────────────────────────────
            headerPanel = new Panel();
            headerPanel.Height = 72;
            headerPanel.Dock = DockStyle.Top;
            headerPanel.BackColor = darkBg;
            this.Controls.Add(headerPanel);

            // ── Scroll container ───────────────────────────────
            mainScrollPanel = new Panel();
            mainScrollPanel.Dock = DockStyle.Fill;
            mainScrollPanel.AutoScroll = true;
            mainScrollPanel.BackColor = lightBeige;
            this.Controls.Add(mainScrollPanel);

            mainScrollPanel.BringToFront();
            headerPanel.BringToFront();

            CreateHeader();
            CreateHeroSection();
            CreateAboutSection();
            CreateStatsSection();
            CreateDishesSection();
            CreateServiceSection();
            CreateContactSection();
            CreateFooterSection();

            ShowPage("HOME");
        }

        private void ShowPage(string page)
        {
            // Hide all content panels first
            heroPanel.Visible    = false;
            aboutPanel.Visible   = false;
            statsPanel.Visible   = false;
            dishesPanel.Visible  = false;
            servicePanel.Visible = false;
            contactPanel.Visible = false;
            footerPanel.Visible  = false;

            if (page == "HOME")
            {
                heroPanel.Visible   = true;
                statsPanel.Visible  = true;
                dishesPanel.Visible = true;
                footerPanel.Visible = true;
            }
            else if (page == "ABOUT")
            {
                aboutPanel.Visible  = true;
                footerPanel.Visible = true;
            }
            else if (page == "CONTACT")
            {
                contactPanel.Visible = true;
            }
            else if (page == "SERVICE")
            {
                servicePanel.Visible = true;
            }

            mainScrollPanel.AutoScrollPosition = new Point(0, 0);
            StackPanels();
            mainScrollPanel.Invalidate(true);
        }

        private void StackPanels()
        {
            int currentY = 0;
            Panel[] panels = { heroPanel, aboutPanel, statsPanel, dishesPanel, servicePanel, contactPanel, footerPanel };

            foreach (var p in panels)
            {
                if (p != null && p.Visible)
                {
                    if (p == statsPanel && heroPanel.Visible) currentY += 50;
                    p.Location = new Point(0, currentY);
                    currentY += p.Height;
                }
            }
        }

        // ═══════════════════════════════════════════════════════
        // HEADER
        // ═══════════════════════════════════════════════════════
        private void CreateHeader()
        {
            // Logo icon
            Label logoIcon = new Label();
            logoIcon.Text = "🍳";
            logoIcon.Font = new Font("Segoe UI Emoji", 22);
            logoIcon.AutoSize = true;
            logoIcon.ForeColor = Color.White;
            logoIcon.Location = new Point(28, 18);
            headerPanel.Controls.Add(logoIcon);

            // Logo text
            Label logoLabel = new Label();
            logoLabel.Text = "DBU LAUNCH";
            logoLabel.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            logoLabel.ForeColor = accentOrange;
            logoLabel.AutoSize = true;
            logoLabel.Location = new Point(80, 22);
            headerPanel.Controls.Add(logoLabel);

            // Nav — FlowLayoutPanel keeps items tight and auto-wraps
            navFlow = new FlowLayoutPanel();
            navFlow.Height = 72;
            navFlow.AutoSize = false;
            navFlow.FlowDirection = FlowDirection.LeftToRight;
            navFlow.WrapContents = false;
            navFlow.BackColor = Color.Transparent;
            navFlow.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            navFlow.Padding = new Padding(0);
            navFlow.Margin = new Padding(0);

            string[] navItems = { "HOME", "ABOUT", "SERVICE", "LOGIN", "CONTACT" };
            foreach (string item in navItems)
            {
                Label nav = new Label();
                nav.Text = item;
                nav.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                nav.ForeColor = item == "LOGIN" ? accentOrange : textLight;
                nav.AutoSize = true;
                nav.Cursor = Cursors.Hand;
                nav.Margin = new Padding(10, 0, 10, 0); // 10px gap each side
                nav.TextAlign = ContentAlignment.MiddleCenter;
                nav.Padding = new Padding(0, 24, 0, 0);

                // Hover highlight
                nav.MouseEnter += (s, e) => { if (nav.Text != "LOGIN") nav.ForeColor = accentOrange; };
                nav.MouseLeave += (s, e) => { if (nav.Text != "LOGIN") nav.ForeColor = textLight; };

                if (item == "LOGIN")
                {
                    nav.Click += (s, e) =>
                    {
                        var loginForm = new LoginForm();
                        loginForm.FormClosed += (s2, e2) => this.Show();
                        this.Hide();
                        loginForm.Show();
                    };
                }
                else if (item == "ABOUT")
                {
                    nav.Click += (s, e) => ShowPage("ABOUT");
                }
                else if (item == "HOME")
                {
                    nav.Click += (s, e) => ShowPage("HOME");
                }
                else if (item == "CONTACT")
                {
                    nav.Click += (s, e) => ShowPage("CONTACT");
                }
                else if (item == "SERVICE")
                {
                    nav.Click += (s, e) => ShowPage("SERVICE");
                }

                navFlow.Controls.Add(nav);
            }

            headerPanel.Controls.Add(navFlow);
            PositionNav(); // initial positioning
        }

        private void PositionNav()
        {
            if (navFlow == null) return;
            navFlow.Size = new Size(400, 72);
            navFlow.Location = new Point(headerPanel.Width - 430, 0);
        }

        // ═══════════════════════════════════════════════════════
        // HERO
        // ═══════════════════════════════════════════════════════
        private void CreateHeroSection()
        {
            heroPanel = new Panel();
            heroPanel.Location = new Point(0, 0);
            heroPanel.BackColor = lightBeige;
            mainScrollPanel.Controls.Add(heroPanel);

            lblTitle1 = new Label();
            lblTitle1.Text = "Enjoy Our ";
            lblTitle1.Font = new Font("Segoe UI", 38, FontStyle.Bold);
            lblTitle1.ForeColor = textDark;
            lblTitle1.AutoSize = true;
            heroPanel.Controls.Add(lblTitle1);

            lblTitle2 = new Label();
            lblTitle2.Text = "Delicious";
            lblTitle2.Font = new Font("Segoe UI", 38, FontStyle.Bold);
            lblTitle2.ForeColor = accentOrange;
            lblTitle2.AutoSize = true;
            heroPanel.Controls.Add(lblTitle2);

            lblTitle3 = new Label();
            lblTitle3.Text = "Meal";
            lblTitle3.Font = new Font("Segoe UI", 38, FontStyle.Bold);
            lblTitle3.ForeColor = textDark;
            lblTitle3.AutoSize = true;
            heroPanel.Controls.Add(lblTitle3);

            lblDesc = new Label();
            lblDesc.Text = "DBU LAUNCH delivers premium quality food right to your\ndoorstep. Fast, fresh, and flavorful meals for students\nand staff at Debre Brehan University.";
            lblDesc.Font = new Font("Segoe UI", 13);
            lblDesc.ForeColor = Color.FromArgb(75, 85, 99);
            lblDesc.AutoSize = true;
            heroPanel.Controls.Add(lblDesc);

            orderBtn = new Button();
            orderBtn.Text = "Order Now";
            orderBtn.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            orderBtn.BackColor = accentOrange;
            orderBtn.ForeColor = textDark;
            orderBtn.FlatStyle = FlatStyle.Flat;
            orderBtn.FlatAppearance.BorderSize = 0;
            orderBtn.Size = new Size(170, 48);
            orderBtn.Cursor = Cursors.Hand;
            heroPanel.Controls.Add(orderBtn);

            // Building image
            string imgPath = System.IO.Path.Combine(Application.StartupPath, "img", "picdescktop.jpg");
            buildingPic = new PictureBox();
            buildingPic.Size = new Size(480, 320);
            buildingPic.SizeMode = PictureBoxSizeMode.StretchImage;
            buildingPic.BackColor = Color.FromArgb(30, 30, 30);
            if (System.IO.File.Exists(imgPath))
                buildingPic.Image = Image.FromFile(imgPath);
            buildingPic.Paint += RoundedPanel_Paint;
            heroPanel.Controls.Add(buildingPic);

            LayoutHero();
        }

        private void LayoutHero()
        {
            if (heroPanel == null) return;
            int w = mainScrollPanel.ClientSize.Width;
            if (w <= 0) w = this.ClientSize.Width;

            heroPanel.Size = new Size(w, HERO_H);

            int leftX = Math.Max(40, w / 20);

            // Left content zone ends at 48% of form width — hard wall, image cannot cross it
            int leftZoneRight = (int)(w * 0.48);
            int leftZoneWidth = leftZoneRight - leftX;

            // Title row — put "Delicious" on same line only if it fits in left zone
            lblTitle1.AutoSize = true;
            lblTitle1.Location = new Point(leftX, 160);
            int t2X = leftX + lblTitle1.PreferredWidth;
            if (t2X + lblTitle2.PreferredWidth <= leftZoneRight)
            {
                lblTitle2.Location = new Point(t2X, 160);
                lblTitle3.Location = new Point(leftX, 225);
            }
            else
            {
                // Stack vertically — no bleed into image area
                lblTitle2.Location = new Point(leftX, 220);
                lblTitle3.Location = new Point(leftX, 280);
            }

            // Description wraps inside left zone
            lblDesc.MaximumSize = new Size(leftZoneWidth, 0);
            lblDesc.Location    = new Point(leftX, 360);
            orderBtn.Location   = new Point(leftX, 490);

            // Image zone: starts after left content, uses remaining space
            int picStartX = leftZoneRight + 20;
            int picW      = w - picStartX - 30;
            int picH      = (int)(picW * 0.65);
            int picY      = Math.Max(60, (HERO_H - picH) / 2);

            if (picW < 140)   // too narrow — hide instead of overlapping
            {
                buildingPic.Visible = false;
            }
            else
            {
                buildingPic.Visible = true;
                buildingPic.Size     = new Size(picW, picH);
                buildingPic.Location = new Point(picStartX, 160);
            }
        }

        // ═══════════════════════════════════════════════════════
        // ABOUT US
        // ═══════════════════════════════════════════════════════
        private void CreateAboutSection()
        {
            aboutPanel = new Panel();
            aboutPanel.BackColor = lightBeige;
            mainScrollPanel.Controls.Add(aboutPanel);
            
            // Top Dark Box
            Panel darkBox = new Panel();
            darkBox.BackColor = Color.FromArgb(44, 62, 80);
            darkBox.Name = "darkBox";
            aboutPanel.Controls.Add(darkBox);
            
            Label title = new Label();
            title.Text = "Nourishing Minds, Building Community";
            title.Font = new Font("Segoe UI", 32, FontStyle.Bold);
            title.ForeColor = Color.White;
            title.AutoSize = true;
            title.Name = "aboTitle";
            darkBox.Controls.Add(title);
            
            Label mTitle = new Label { Text = "Our Mission", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Name = "mTitle" };
            Label mDesc = new Label { Text = "To provide nutritious, culturally-rich meals that\nfuel academic success and foster campus\ncommunity at Debre Birhan University.", Font = new Font("Segoe UI", 11), ForeColor = Color.LightGray, AutoSize = true, Name = "mDesc" };
            darkBox.Controls.Add(mTitle);
            darkBox.Controls.Add(mDesc);

            Label vTitle = new Label { Text = "Our Vision", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Name = "vTitle" };
            Label vDesc = new Label { Text = "To be Ethiopia's leading model for sustainable\ncampus dining through innovation and student-\ncentered service.", Font = new Font("Segoe UI", 11), ForeColor = Color.LightGray, AutoSize = true, Name = "vDesc" };
            darkBox.Controls.Add(vTitle);
            darkBox.Controls.Add(vDesc);

            // Why Choose title
            Label wTitle = new Label { Text = "Why Choose Our System", Font = new Font("Segoe UI", 24, FontStyle.Bold), ForeColor = textDark, AutoSize = true, Name = "wTitle" };
            aboutPanel.Controls.Add(wTitle);
            Panel wLine = new Panel { BackColor = Color.SeaGreen, Size = new Size(60, 4), Name = "wLine" };
            aboutPanel.Controls.Add(wLine);

            // Three Cards
            string[] cTitles = { "Diverse Menus", "Digital Convenience", "Sustainable Practices" };
            string[] cDescs = { 
                "Daily selection of traditional Ethiopian\ndishes and international cuisine, with\nvegetarian/vegan options.", 
                "Mobile app for meal tracking,\npayments, and real-time menu\nupdates.", 
                "More locally sourced ingredients and\nzero-waste kitchen initiatives." 
            };
            string[] cIcons = { "🍽️", "📱", "🌱" };

            for (int i=0; i<3; i++) {
                Panel card = new Panel { BackColor = Color.White, Size = new Size(300, 220), Name = $"wCard{i}" };
                card.Paint += RoundedPanel_Paint;
                Label ic = new Label { Text = cIcons[i], Font = new Font("Segoe UI Emoji", 32), AutoSize = true, Location = new Point(130, 20) };
                Label t = new Label { Text = cTitles[i], Font = new Font("Segoe UI", 13, FontStyle.Bold), ForeColor = textDark, AutoSize = true };
                // Center labels horizontal
                card.Controls.Add(ic);
                card.Controls.Add(t);
                Label d = new Label { Text = cDescs[i], Font = new Font("Segoe UI", 10), ForeColor = Color.Gray, AutoSize = true, TextAlign = ContentAlignment.MiddleCenter };
                card.Controls.Add(d);
                aboutPanel.Controls.Add(card);
            }

            LayoutAbout();
        }

        private void LayoutAbout()
        {
            if (aboutPanel == null) return;
            int w = mainScrollPanel.ClientSize.Width;
            if (w <= 0) w = this.ClientSize.Width;

            aboutPanel.Size = new Size(w, ABOUT_H);

            Panel db = (Panel)aboutPanel.Controls["darkBox"];
            if (db != null) {
                int dbW = Math.Min(1000, w - 80);
                db.Size = new Size(dbW, 350);
                db.Location = new Point((w - dbW) / 2, 100);
                
                // Need to use safe rounding or simple rectangle since SetRoundedRegion is defined elsewhere
                // Actually the hero building uses RoundedPanel_Paint so:
                db.Paint -= RoundedPanel_Paint; // Ensure no duplicates
                db.Paint += RoundedPanel_Paint;

                Control t = db.Controls["aboTitle"];
                if (t != null) t.Location = new Point((dbW - t.Width)/2, 60);

                int colW = dbW / 2;
                Control mt = db.Controls["mTitle"], md = db.Controls["mDesc"];
                if (mt != null) mt.Location = new Point(60, 160);
                if (md != null && mt != null) md.Location = new Point(60, mt.Bottom + 15);

                Control vt = db.Controls["vTitle"], vd = db.Controls["vDesc"];
                if (vt != null) vt.Location = new Point(colW, 160);
                if (vd != null && vt != null) vd.Location = new Point(colW, vt.Bottom + 15);
            }

            Control wt = aboutPanel.Controls["wTitle"], wl = aboutPanel.Controls["wLine"];
            if (wt != null) wt.Location = new Point((w - wt.Width)/2, 450);
            if (wl != null && wt != null) wl.Location = new Point((w - wl.Width)/2, wt.Bottom + 5);

            int startX = Math.Max(20, (w - (300 * 3 + 40 * 2)) / 2);
            for (int i=0; i<3; i++) {
                Control c = aboutPanel.Controls[$"wCard{i}"];
                if (c != null) {
                    c.Location = new Point(startX + i * 340, 530);
                    
                    // Center labels
                    Control ic = c.Controls[0], tc = c.Controls[1], dc = c.Controls[2];
                    ic.Location = new Point((300 - ic.Width)/2, 30);
                    tc.Location = new Point((300 - tc.Width)/2, 90);
                    dc.Location = new Point((300 - dc.Width)/2, 130);
                }
            }
        }

        // ═══════════════════════════════════════════════════════
        // STATS
        // ═══════════════════════════════════════════════════════
        private void CreateStatsSection()
        {
            statsPanel = new Panel();
            statsPanel.BackColor = Color.White;
            mainScrollPanel.Controls.Add(statsPanel);

            statsTitle = new Label();
            statsTitle.Text = "We are ready to serve you with our\npremium delivery system";
            statsTitle.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            statsTitle.AutoSize = true;
            statsPanel.Controls.Add(statsTitle);

            statsDesc = new Label();
            statsDesc.Text = "Located in the heart of Debre Brehan University, we provide high-quality\nmeals prepared with fresh ingredients by our master chefs. Our mission\nis to deliver delicious, nutritious food quickly and efficiently.";
            statsDesc.Font = new Font("Segoe UI", 11);
            statsDesc.ForeColor = Color.Gray;
            statsDesc.AutoSize = true;
            statsPanel.Controls.Add(statsDesc);

            string[] nums  = { "10+", "5", "20+" };
            string[] texts = { "Years\nExperience", "Master Chefs", "Menu Items" };
            for (int i = 0; i < 3; i++)
            {
                Panel card = new Panel();
                card.Size = new Size(150, 150);
                card.BackColor = Color.White;
                card.BorderStyle = BorderStyle.FixedSingle;

                Label nLbl = new Label { Text = nums[i], Font = new Font("Segoe UI", 26, FontStyle.Bold), ForeColor = accentOrange, AutoSize = true, Location = new Point(20, 20) };
                Label tLbl = new Label { Text = texts[i], Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = textDark, AutoSize = true, Location = new Point(20, 80) };
                card.Controls.Add(nLbl);
                card.Controls.Add(tLbl);
                statsPanel.Controls.Add(card);
                statCards[i] = card;
            }

            LayoutStats();
        }

        private void LayoutStats()
        {
            if (statsPanel == null) return;
            int w = mainScrollPanel.ClientSize.Width;
            if (w <= 0) w = this.ClientSize.Width;

            statsPanel.Size     = new Size(w, STATS_H);

            int leftX         = Math.Max(40, w / 20);
            int leftZoneWidth = (int)(w * 0.50) - leftX;  // text in left half

            statsTitle.MaximumSize = new Size(leftZoneWidth, 0);
            statsTitle.Location    = new Point(leftX, 40);
            statsDesc.MaximumSize  = new Size(leftZoneWidth, 0);
            statsDesc.Location     = new Point(leftX, 140);

            // Cards: fit 3 inside right half, shrink card width if needed
            int rightStart = (int)(w * 0.52);
            int rightW     = w - rightStart - leftX;
            int cardGap    = 12;
            int cardW      = Math.Max(90, (rightW - 2 * cardGap) / 3);
            for (int i = 0; i < 3; i++)
            {
                statCards[i].Size     = new Size(cardW, 145);
                statCards[i].Location = new Point(rightStart + i * (cardW + cardGap), 70);
            }
        }

        // ═══════════════════════════════════════════════════════
        // DISHES
        // ═══════════════════════════════════════════════════════
        private void CreateDishesSection()
        {
            dishesPanel = new Panel();
            dishesPanel.BackColor = lightBeige;
            mainScrollPanel.Controls.Add(dishesPanel);

            dishUnderline = new Panel { Size = new Size(340, 4), BackColor = accentOrange };
            dishesPanel.Controls.Add(dishUnderline);

            dishSectionTitle = new Label();
            dishSectionTitle.Text = "Our Popular Dishes";
            dishSectionTitle.Font = new Font("Segoe UI", 26, FontStyle.Bold);
            dishSectionTitle.AutoSize = true;
            dishesPanel.Controls.Add(dishSectionTitle);

            string[] dishImages = { "chechebsa.jpg", "fulll.jpg", "koker.jpg" };
            string[] dishNames  = { "Chechebsa", "Full Ethiopian", "Koker" };

            for (int i = 0; i < 3; i++)
            {
                string path = System.IO.Path.Combine(Application.StartupPath, "img", dishImages[i]);

                Panel card = new Panel();
                card.Size = new Size(300, 260);
                card.BackColor = Color.White;
                card.Paint += RoundedPanel_Paint;

                PictureBox pic = new PictureBox();
                pic.Size = new Size(300, 200);
                pic.Location = new Point(0, 0);
                pic.SizeMode = PictureBoxSizeMode.StretchImage;
                pic.BackColor = Color.FromArgb(230, 230, 230);
                if (System.IO.File.Exists(path))
                    pic.Image = Image.FromFile(path);
                pic.Paint += RoundedPanel_Paint;
                card.Controls.Add(pic);

                Label nameLbl = new Label();
                nameLbl.Text = dishNames[i];
                nameLbl.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                nameLbl.ForeColor = textDark;
                nameLbl.AutoSize = true;
                nameLbl.Location = new Point(12, 212);
                card.Controls.Add(nameLbl);

                dishesPanel.Controls.Add(card);
                dishCards[i] = card;
            }

            moreBtn = new Button();
            moreBtn.Text = "MORE MENU\nITEMS";
            moreBtn.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            moreBtn.BackColor = accentOrange;
            moreBtn.ForeColor = textDark;
            moreBtn.FlatStyle = FlatStyle.Flat;
            moreBtn.FlatAppearance.BorderSize = 0;
            moreBtn.Size = new Size(180, 55);
            moreBtn.Cursor = Cursors.Hand;
            moreBtn.Paint += RoundedPanel_Paint;
            dishesPanel.Controls.Add(moreBtn);
            moreBtn.BringToFront();

            LayoutDishes();
        }

        private void LayoutDishes()
        {
            if (dishesPanel == null) return;
            int w = mainScrollPanel.ClientSize.Width;
            if (w <= 0) w = this.ClientSize.Width;

            dishesPanel.Size = new Size(w, DISHES_H);

            // Title centered
            dishSectionTitle.Location = new Point((w - dishSectionTitle.PreferredWidth) / 2, 110);
            dishUnderline.Location = new Point((w - dishUnderline.Width) / 2, 100);

            // Cards — evenly distributed with equal margins
            int margin = Math.Max(20, (w - 3 * 300) / 4);
            for (int i = 0; i < 3; i++)
                dishCards[i].Location = new Point(margin + i * (300 + margin), 180);

            // "More" button centered
            moreBtn.Location = new Point((w - moreBtn.Width) / 2, 468);
        }

        // ═══════════════════════════════════════════════════════
        // FOOTER
        // ═══════════════════════════════════════════════════════
        private void CreateFooterSection()
        {
            footerPanel = new Panel();
            footerPanel.BackColor = darkBg;
            mainScrollPanel.Controls.Add(footerPanel);

            string[] cols  = { "Company", "Contact", "Opening Hours", "Newsletter" };
            string[] texts =
            {
                "About Us\n\nContact Us\n\nReservation\n\nPrivacy Policy",
                "123 Street, DEBRE BIRHAN\nUNIVERSITY\n\n+125976542311\n\ninfo@dbulaunch.com",
                "Monday - Sunday:\n6:00 AM - 12:00 PM\n\nDelivery Hours:\n7:00 AM - 11:30 PM",
                "Subscribe to our newsletter\nfor special offers and updates."
            };

            for (int i = 0; i < 4; i++)
            {
                Label h = new Label { Text = cols[i], Font = new Font("Segoe UI", 13, FontStyle.Bold), ForeColor = accentOrange, AutoSize = true };
                Label b = new Label { Text = texts[i], Font = new Font("Segoe UI", 10), ForeColor = Color.Silver, AutoSize = true };
                footerPanel.Controls.Add(h);
                footerPanel.Controls.Add(b);
            }

            // Newsletter input
            TextBox emailBox = new TextBox();
            emailBox.Size = new Size(180, 28);
            emailBox.Font = new Font("Segoe UI", 11);
            footerPanel.Controls.Add(emailBox);

            Button sendBtn = new Button();
            sendBtn.Size = new Size(46, 28);
            sendBtn.BackColor = accentOrange;
            sendBtn.FlatStyle = FlatStyle.Flat;
            sendBtn.FlatAppearance.BorderSize = 0;
            sendBtn.Text = "➤";
            footerPanel.Controls.Add(sendBtn);

            copyRight = new Label();
            copyRight.Text = "© 2024 DBU LAUNCH. All Rights Reserved.";
            copyRight.ForeColor = Color.DimGray;
            copyRight.AutoSize = true;
            copyRight.Font = new Font("Segoe UI", 10);
            footerPanel.Controls.Add(copyRight);

            LayoutFooter();
        }

        private void LayoutFooter()
        {
            if (footerPanel == null) return;
            int w = mainScrollPanel.ClientSize.Width;
            if (w <= 0) w = this.ClientSize.Width;

            footerPanel.Size = new Size(w, FOOTER_H);

            int colW = w / 4;
            // footerPanel has: 4 headers + 4 bodies + emailBox + sendBtn + copyright
            var controls = footerPanel.Controls;
            for (int i = 0; i < 4; i++)
            {
                controls[i * 2].Location = new Point(40 + i * colW, 60);       // headers
                controls[i * 2 + 1].Location = new Point(40 + i * colW, 110);  // bodies
            }

            // email + send sit below newsletter (col 3)
            Control emailBox = controls[8];
            Control sendBtn  = controls[9];
            emailBox.Location = new Point(40 + 3 * colW, 230);
            sendBtn.Location  = new Point(40 + 3 * colW + 184, 230);

            // copyright centered
            copyRight.Location = new Point((w - copyRight.PreferredWidth) / 2, FOOTER_H - 45);
        }

        // ═══════════════════════════════════════════════════════
        // RESIZE
        // ═══════════════════════════════════════════════════════
        private void LandingForm_Resize(object sender, EventArgs e)
        {
            if (mainScrollPanel == null) return;

            PositionNav();
            LayoutHero();
            LayoutAbout();
            LayoutStats();
            LayoutDishes();
            LayoutService();
            LayoutContact();
            LayoutFooter();
            StackPanels();

            mainScrollPanel.Invalidate(true);
        }

        // ═══════════════════════════════════════════════════════
        // SERVICE / MENU
        // ═══════════════════════════════════════════════════════
        private void CreateServiceSection()
        {
            servicePanel = new Panel { BackColor = Color.FromArgb(248, 249, 252), Name = "svcPanel" };
            mainScrollPanel.Controls.Add(servicePanel);

            // ── Page heading ────────────────────────────────────────────────
            Label svcTitle = new Label
            {
                Text = "Our Menu",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = textDark, AutoSize = true, Name = "svcTitle"
            };
            servicePanel.Controls.Add(svcTitle);

            Panel titleLine = new Panel { BackColor = accentOrange, Size = new Size(60, 4), Name = "svcLine" };
            servicePanel.Controls.Add(titleLine);

            Label svcSub = new Label
            {
                Text = "Fresh, delicious meals prepared daily for DBU students and staff",
                Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(100, 116, 139),
                AutoSize = true, Name = "svcSub"
            };
            servicePanel.Controls.Add(svcSub);

            // ── Cart button (top-right) ─────────────────────────────────────────
            _svcCartBtn = new Button
            {
                Text = "\ud83d\uded2  Cart (0)",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = accentOrange, ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand,
                Size = new Size(150, 42), Name = "svcCartBtn"
            };
            _svcCartBtn.FlatAppearance.BorderSize = 0;
            _svcCartBtn.Click += (s, e) => ShowCartOverlay();
            servicePanel.Controls.Add(_svcCartBtn);

            // ── Category filter buttons ────────────────────────────────────────
            string[] filters = { "All", "Ethiopian", "International", "Snacks", "Beverages" };
            for (int i = 0; i < filters.Length; i++)
            {
                Button fb = new Button
                {
                    Text = filters[i],
                    Font = new Font("Segoe UI", 10, i == 0 ? FontStyle.Bold : FontStyle.Regular),
                    BackColor = i == 0 ? accentOrange : Color.White,
                    ForeColor = i == 0 ? Color.White : Color.FromArgb(60, 70, 90),
                    FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand,
                    Size = new Size(118, 38), Name = $"svcFilter{i}"
                };
                fb.FlatAppearance.BorderSize = 1;
                fb.FlatAppearance.BorderColor = Color.FromArgb(210, 215, 225);
                servicePanel.Controls.Add(fb);
            }

            // ── Card grid (FlowLayoutPanel) ──────────────────────────────────────
            _svcGrid = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents  = true,
                AutoSize      = true,
                AutoSizeMode  = AutoSizeMode.GrowAndShrink,
                BackColor     = Color.Transparent,
                Name          = "svcGrid"
            };
            servicePanel.Controls.Add(_svcGrid);

            // ── Food data ──────────────────────────────────────────────────
            var foods = new[]
            {
                new { Name = "Injera & Doro Wat",      Cat = "Ethiopian",     Stars = 5, Price = "85 ETB",  Tag = "Popular",      Img = "https://images.unsplash.com/photo-1585937421612-70a008356fbe?w=300&h=200&fit=crop" },
                new { Name = "Chechebsa",              Cat = "Ethiopian",     Stars = 4, Price = "45 ETB",  Tag = "Breakfast",    Img = "https://images.unsplash.com/photo-1567620905732-2d1ec7ab7445?w=300&h=200&fit=crop" },
                new { Name = "Shiro Wat",              Cat = "Ethiopian",     Stars = 5, Price = "55 ETB",  Tag = "",             Img = "https://images.unsplash.com/photo-1540189549336-e6e99c3679fe?w=300&h=200&fit=crop" },
                new { Name = "Firfir",                 Cat = "Ethiopian",     Stars = 4, Price = "50 ETB",  Tag = "Morning",      Img = "https://images.unsplash.com/photo-1484723091739-30a097e8f929?w=300&h=200&fit=crop" },
                new { Name = "Grilled Tilapia",        Cat = "International", Stars = 4, Price = "120 ETB", Tag = "Chef Special", Img = "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?w=300&h=200&fit=crop" },
                new { Name = "Pasta Bolognese",        Cat = "International", Stars = 4, Price = "95 ETB",  Tag = "",             Img = "https://images.unsplash.com/photo-1473093295043-cdd812d0e601?w=300&h=200&fit=crop" },
                new { Name = "Garden Salad",           Cat = "International", Stars = 3, Price = "40 ETB",  Tag = "Healthy",      Img = "https://images.unsplash.com/photo-1565958011703-44f9829ba187?w=300&h=200&fit=crop" },
                new { Name = "Beef Burger",            Cat = "International", Stars = 5, Price = "85 ETB",  Tag = "Bestseller",   Img = "https://images.unsplash.com/photo-1568901346375-23c9450c58cd?w=300&h=200&fit=crop" },
                new { Name = "Sambusa (3 pcs)",        Cat = "Snacks",        Stars = 5, Price = "30 ETB",  Tag = "",             Img = "https://images.unsplash.com/photo-1565299624946-b28f40a0ae38?w=300&h=200&fit=crop" },
                new { Name = "Fatira with Honey",      Cat = "Snacks",        Stars = 4, Price = "35 ETB",  Tag = "Sweet",        Img = "https://images.unsplash.com/photo-1621303837174-89787a7d4729?w=300&h=200&fit=crop" },
                new { Name = "Fresh Mango Juice",      Cat = "Beverages",     Stars = 5, Price = "35 ETB",  Tag = "",             Img = "https://images.unsplash.com/photo-1482049016688-2d3e1b311543?w=300&h=200&fit=crop" },
                new { Name = "Ethiopian Coffee",       Cat = "Beverages",     Stars = 5, Price = "15 ETB",  Tag = "Hot",          Img = "https://images.unsplash.com/photo-1559525839-b184a4d698c7?w=300&h=200&fit=crop" },
            };

            foreach (var f in foods)
                _svcGrid.Controls.Add(MkFoodCard(f.Name, f.Cat, f.Stars, f.Price, f.Tag, f.Img));

            // Setup Filter clicks
            for (int i = 0; i < filters.Length; i++)
            {
                Button fb = (Button)servicePanel.Controls[$"svcFilter{i}"];
                fb.Click += (s, e) =>
                {
                    // Reset styling
                    for (int j = 0; j < filters.Length; j++)
                    {
                        Button other = (Button)servicePanel.Controls[$"svcFilter{j}"];
                        other.BackColor = Color.White;
                        other.ForeColor = Color.FromArgb(60, 70, 90);
                        other.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                    }
                    // Apply active style
                    fb.BackColor = accentOrange;
                    fb.ForeColor = Color.White;
                    fb.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                    // Filter cards
                    string selectedCat = fb.Text;
                    foreach (Control card in _svcGrid.Controls)
                    {
                        if (selectedCat == "All") card.Visible = true;
                        else card.Visible = (card.Tag as string == selectedCat);
                    }

                    // Force re-layout
                    LayoutService();
                };
            }

            LayoutService();
        }

        private Panel MkFoodCard(string name, string cat, int stars, string price, string tag, string imgUrl)
        {
            const int CW = 240, CH = 330;
            Color cardShadow = Color.FromArgb(226, 232, 240);

            Panel card = new Panel
            {
                Size      = new Size(CW, CH),
                BackColor = Color.White,
                Margin    = new Padding(12),
                Cursor    = Cursors.Hand,
                Tag       = cat // Used for filtering
            };
            card.Paint += RoundedPanel_Paint;

            // ─ Image ────────────────────────────────────────────────────────────
            PictureBox pic = new PictureBox
            {
                Size     = new Size(CW, 165),
                Location = new Point(0, 0),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = cardShadow
            };
            try { pic.LoadAsync(imgUrl); } catch { }
            pic.Paint += RoundedPanel_Paint;
            card.Controls.Add(pic);

            // ─ Tag badge ──────────────────────────────────────────────────────
            if (!string.IsNullOrEmpty(tag))
            {
                Color tagBg = tag == "Popular" || tag == "Bestseller" ? Color.FromArgb(220, 38, 38)
                            : tag == "Healthy"                        ? Color.FromArgb(34, 197, 94)
                            : tag == "Chef Special"                   ? Color.FromArgb(124, 58, 237)
                            : accentOrange;
                Label tagLbl = new Label
                {
                    Text = " " + tag + " ",
                    Font = new Font("Segoe UI", 8, FontStyle.Bold),
                    ForeColor = Color.White, BackColor = tagBg,
                    AutoSize = true, Location = new Point(10, 10)
                };
                pic.Controls.Add(tagLbl);
            }

            // ─ Category chip ───────────────────────────────────────────────────
            Label catLbl = new Label
            {
                Text = cat, AutoSize = true,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(37, 99, 235),
                BackColor = Color.FromArgb(219, 234, 254),
                Location  = new Point(12, 174)
            };
            card.Controls.Add(catLbl);

            // ─ Name ────────────────────────────────────────────────────────────
            Label nameLbl = new Label
            {
                Text = name, AutoSize = false,
                Font      = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = textDark,
                Size      = new Size(CW - 24, 42),
                Location  = new Point(12, 196)
            };
            card.Controls.Add(nameLbl);

            // ─ Star rating ────────────────────────────────────────────────────
            string starStr = new string('\u2605', stars) + new string('\u2606', 5 - stars);
            Label starsLbl = new Label
            {
                Text = starStr + $"  ({stars}.0)",
                AutoSize = true,
                Font     = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(234, 179, 8),
                Location  = new Point(12, 240)
            };
            card.Controls.Add(starsLbl);

            // ─ Price ───────────────────────────────────────────────────────────
            Label priceLbl = new Label
            {
                Text = price, AutoSize = true,
                Font      = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = accentOrange,
                Location  = new Point(12, 262)
            };
            card.Controls.Add(priceLbl);

            // ─ Add to Cart button ──────────────────────────────────────────
            Button addBtn = new Button
            {
                Text      = "\ud83d\uded2  Add to Cart",
                Font      = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = accentOrange, ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand,
                Size      = new Size(CW - 24, 38),
                Location  = new Point(12, 284)
            };
            addBtn.FlatAppearance.BorderSize = 0;
            
            // Extract numeric price
            int priceVal = 0;
            if (price.Contains(" ")) int.TryParse(price.Split(' ')[0], out priceVal);
            string capturedName = name;
            
            addBtn.Click += (s, e) =>
            {
                var existing = _cartItems.Find(c => c.Name == capturedName);
                if (existing != null) existing.Quantity++;
                else _cartItems.Add(new CartItem { Name = capturedName, Price = priceVal, Quantity = 1 });

                _cartCount++;
                _svcCartBtn.Text = $"\ud83d\uded2  Cart ({_cartCount})";
                UIHelper.ShowToast($"{capturedName} added to cart!");
            };
            card.Controls.Add(addBtn);

            return card;
        }

        private void LayoutService()
        {
            if (servicePanel == null) return;
            int w = mainScrollPanel.ClientSize.Width;
            if (w <= 0) w = this.ClientSize.Width;

            // ─ Heading ──────────────────────────────────────────────────
            Control title = servicePanel.Controls["svcTitle"];
            Control line  = servicePanel.Controls["svcLine"];
            Control sub   = servicePanel.Controls["svcSub"];
            Control cart  = servicePanel.Controls["svcCartBtn"];

            // Push everything down by shifting title Y to 80
            if (title != null) title.Location = new Point((w - title.Width) / 2, 80);
            if (line  != null) line.Location  = new Point((w - line.Width)  / 2, title != null ? title.Bottom + 4  : 130);
            if (sub   != null) sub.Location   = new Point((w - sub.Width)   / 2, line  != null ? line.Bottom  + 8  : 150);
            if (cart  != null) cart.Location  = new Point(w - cart.Width - 40, 80);

            // ─ Filter buttons ─────────────────────────────────────────────
            const int FB_W = 118, FB_GAP = 10, FB_COUNT = 5;
            int filterY      = sub  != null ? sub.Bottom + 28 : 190;
            int totalFilterW = FB_COUNT * FB_W + (FB_COUNT - 1) * FB_GAP;
            int filterStartX = Math.Max(40, (w - totalFilterW) / 2);

            for (int i = 0; i < FB_COUNT; i++)
            {
                Control fb = servicePanel.Controls[$"svcFilter{i}"];
                if (fb != null) fb.Location = new Point(filterStartX + i * (FB_W + FB_GAP), filterY);
            }

            // ─ Card grid ──────────────────────────────────────────────────
            if (_svcGrid != null)
            {
                int gridY = filterY + 56;
                // Calculate exact centered width based on card size + margins
                // Each card width = 240, margin = 12 total H = 264
                int cardTotalW = 264;
                int maxCardsOnLine = Math.Max(1, (w - 40) / cardTotalW); 
                int exactGridW = maxCardsOnLine * cardTotalW;

                _svcGrid.Location = new Point(Math.Max(20, (w - exactGridW) / 2), gridY);
                _svcGrid.MaximumSize = new Size(exactGridW, 9999);
                _svcGrid.Size = new Size(exactGridW, _svcGrid.PreferredSize.Height);

                int panelH = gridY + _svcGrid.Height + 50;
                servicePanel.Size = new Size(w, Math.Max(panelH, 600));
            }
            else
            {
                servicePanel.Size = new Size(w, 600);
            }
        }

        private void ShowCartOverlay()
        {
            if (_cartOverlay == null)
            {
                _cartOverlay = new Panel { BackColor = Color.FromArgb(160, 0, 0, 0), Dock = DockStyle.Fill };
                this.Controls.Add(_cartOverlay);
            }
            
            _cartOverlay.Controls.Clear();
            _cartOverlay.Visible = true;
            _cartOverlay.BringToFront();

            // Background click closes overlay
            Panel bgClickArea = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            bgClickArea.Click += (s, e) => _cartOverlay.Visible = false;
            _cartOverlay.Controls.Add(bgClickArea);

            // Dialog Card
            Panel dialog = new Panel { BackColor = Color.White, Size = new Size(540, 500) };
            dialog.Location = new Point((_cartOverlay.Width - dialog.Width) / 2, (_cartOverlay.Height - dialog.Height) / 2);
            dialog.Paint += RoundedPanel_Paint;
            bgClickArea.Controls.Add(dialog); // Add it to bg to physically exist, or we can add to overlay and handle clicks cleanly.
            
            _cartOverlay.Controls.Add(dialog);
            dialog.BringToFront(); // prevent bg click from overlapping dialog

            Label title = new Label { Text = "Your Order Cart", Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = textDark, AutoSize = true, Location = new Point(30, 25) };
            dialog.Controls.Add(title);

            Button closeBtn = new Button { Text = "\u2715", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(156, 163, 175), BackColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(40, 40), Location = new Point(dialog.Width - 60, 20), Cursor = Cursors.Hand };
            closeBtn.FlatAppearance.BorderSize = 0;
            closeBtn.Click += (s, e) => _cartOverlay.Visible = false;
            closeBtn.MouseEnter += (s, e) => closeBtn.ForeColor = Color.Red;
            closeBtn.MouseLeave += (s, e) => closeBtn.ForeColor = Color.FromArgb(156, 163, 175);
            dialog.Controls.Add(closeBtn);

            Panel divLine = new Panel { BackColor = Color.FromArgb(229, 231, 235), Size = new Size(480, 2), Location = new Point(30, 75) };
            dialog.Controls.Add(divLine);

            if (_cartItems.Count == 0 || _cartCount == 0)
            {
                Label emptyLbl = new Label { Text = "There are no orders in your cart.", Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(107, 114, 128), AutoSize = true };
                emptyLbl.Location = new Point((dialog.Width - 250) / 2, 220); // rough center
                dialog.Controls.Add(emptyLbl);
                
                Button contBtn = new Button { Text = "Browse Menu", Font = new Font("Segoe UI", 11, FontStyle.Bold), BackColor = accentOrange, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(160, 42), Location = new Point((dialog.Width - 160) / 2, 270), Cursor = Cursors.Hand };
                contBtn.FlatAppearance.BorderSize = 0;
                contBtn.Click += (s, e) => _cartOverlay.Visible = false;
                dialog.Controls.Add(contBtn);
            }
            else
            {
                Panel listPan = new Panel { AutoScroll = true, Size = new Size(480, 280), Location = new Point(30, 90) };
                dialog.Controls.Add(listPan);

                int cy = 0;
                int total = 0;
                foreach (var item in _cartItems)
                {
                    Panel itemPan = new Panel { Size = new Size(460, 50), Location = new Point(10, cy), BackColor = Color.FromArgb(249, 250, 251) };
                    itemPan.Paint += RoundedPanel_Paint;
                    listPan.Controls.Add(itemPan);

                    Label qtyLbl = new Label { Text = $"{item.Quantity}x", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = accentOrange, AutoSize = true, Location = new Point(15, 13) };
                    Label nmLbl = new Label { Text = item.Name, Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = textDark, AutoSize = true, Location = new Point(50, 13) };

                    int lineTotal = item.Price * item.Quantity;
                    Label prLbl = new Label { Text = $"{lineTotal} ETB", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = textDark, AutoSize = false, TextAlign = ContentAlignment.TopRight, Size = new Size(100, 25), Location = new Point(340, 13) };
                    
                    itemPan.Controls.Add(qtyLbl);
                    itemPan.Controls.Add(nmLbl);
                    itemPan.Controls.Add(prLbl);

                    cy += 60;
                    total += lineTotal;
                }

                Panel bottomLine = new Panel { BackColor = Color.FromArgb(229, 231, 235), Size = new Size(480, 2), Location = new Point(30, 380) };
                dialog.Controls.Add(bottomLine);

                Label totTxt = new Label { Text = "Total Amount", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(107, 114, 128), AutoSize = true, Location = new Point(30, 400) };
                Label totVal = new Label { Text = $"{total} ETB", Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = accentOrange, AutoSize = false, TextAlign = ContentAlignment.TopRight, Size = new Size(150, 30), Location = new Point(360, 396) };
                dialog.Controls.Add(totTxt);
                dialog.Controls.Add(totVal);

                Button payBtn = new Button { Text = "Proceed to Payment \u2192", Font = new Font("Segoe UI", 12, FontStyle.Bold), BackColor = Color.FromArgb(37, 99, 235), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(480, 50), Location = new Point(30, 465), Cursor = Cursors.Hand };
                payBtn.FlatAppearance.BorderSize = 0;
                payBtn.Click += (s, e) => {
                    _cartOverlay.Visible = false;
                    ShowPaymentOverlay(total);
                };

                // we need robust positioning for payment button, modify dialog size
                dialog.Size = new Size(540, 550);
                dialog.Location = new Point((_cartOverlay.Width - dialog.Size.Width) / 2, (_cartOverlay.Height - dialog.Size.Height) / 2);
                dialog.Controls.Add(payBtn);
            }
        }

        private void ShowPaymentOverlay(int totalAmount)
        {
            if (_paymentOverlay == null)
            {
                _paymentOverlay = new Panel { BackColor = Color.FromArgb(160, 0, 0, 0), Dock = DockStyle.Fill };
                this.Controls.Add(_paymentOverlay);
            }

            _paymentOverlay.Controls.Clear();
            _paymentOverlay.Visible = true;
            _paymentOverlay.BringToFront();

            Panel bgClickArea = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            bgClickArea.Click += (s, e) => _paymentOverlay.Visible = false;
            _paymentOverlay.Controls.Add(bgClickArea);

            Panel dialog = new Panel { BackColor = Color.White, Size = new Size(460, 560) };
            dialog.Location = new Point((_paymentOverlay.Width - dialog.Width) / 2, (_paymentOverlay.Height - dialog.Height) / 2);
            dialog.Paint += RoundedPanel_Paint;
            bgClickArea.Controls.Add(dialog);

            _paymentOverlay.Controls.Add(dialog);
            dialog.BringToFront();

            Label title = new Label { Text = "Payment Details", Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = textDark, AutoSize = true, Location = new Point(30, 25) };
            dialog.Controls.Add(title);

            Button closeBtn = new Button { Text = "\u2715", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(156, 163, 175), BackColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(40, 40), Location = new Point(dialog.Width - 60, 20), Cursor = Cursors.Hand };
            closeBtn.FlatAppearance.BorderSize = 0;
            closeBtn.Click += (s, e) => _paymentOverlay.Visible = false;
            dialog.Controls.Add(closeBtn);

            Panel divLine = new Panel { BackColor = Color.FromArgb(229, 231, 235), Size = new Size(400, 2), Location = new Point(30, 75) };
            dialog.Controls.Add(divLine);

            Label totalTxt = new Label { Text = "Total to Pay", Font = new Font("Segoe UI", 12), ForeColor = Color.FromArgb(107, 114, 128), AutoSize = true, Location = new Point(30, 95) };
            Label totalVal = new Label { Text = $"{totalAmount} ETB", Font = new Font("Segoe UI", 24, FontStyle.Bold), ForeColor = accentOrange, AutoSize = true, Location = new Point(30, 120) };
            dialog.Controls.Add(totalTxt);
            dialog.Controls.Add(totalVal);

            Label methLbl = new Label { Text = "Select Payment Method", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = textDark, AutoSize = true, Location = new Point(30, 180) };
            dialog.Controls.Add(methLbl);

            string[] methods = { "Telebirr", "CBE Birr", "Cash on Delivery" };
            int radioY = 215;
            RadioButton firstRadio = null;

            foreach (var m in methods)
            {
                Panel rbPan = new Panel { Size = new Size(400, 50), Location = new Point(30, radioY), BackColor = Color.FromArgb(248, 250, 252) };
                rbPan.Paint += RoundedPanel_Paint;
                dialog.Controls.Add(rbPan);

                RadioButton rb = new RadioButton { Text = m, Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(55, 65, 81), AutoSize = true, Location = new Point(20, 12), Cursor = Cursors.Hand };
                rbPan.Controls.Add(rb);

                if (firstRadio == null) { firstRadio = rb; rb.Checked = true; }
                radioY += 60;
            }

            Label phLbl = new Label { Text = "Phone Number (for digital payment)", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(107, 114, 128), AutoSize = true, Location = new Point(30, 410) };
            dialog.Controls.Add(phLbl);

            TextBox phoneBox = new TextBox { Font = new Font("Segoe UI", 14), Size = new Size(400, 35), Location = new Point(30, 435), BorderStyle = BorderStyle.FixedSingle, BackColor = Color.White };
            dialog.Controls.Add(phoneBox);

            Panel bottomLine = new Panel { BackColor = Color.FromArgb(229, 231, 235), Size = new Size(400, 2), Location = new Point(30, 490) };
            dialog.Controls.Add(bottomLine);

            Button confBtn = new Button { Text = "Confirm Order", Font = new Font("Segoe UI", 12, FontStyle.Bold), BackColor = Color.FromArgb(34, 197, 94), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(400, 50), Location = new Point(30, 520), Cursor = Cursors.Hand };
            dialog.Size = new Size(460, 610);
            dialog.Location = new Point((_paymentOverlay.Width - dialog.Width) / 2, (_paymentOverlay.Height - dialog.Height) / 2);
            confBtn.FlatAppearance.BorderSize = 0;
            confBtn.Click += (s, e) => {
                if (firstRadio.Checked == false && string.IsNullOrWhiteSpace(phoneBox.Text) && !dialog.Controls[7].Text.Contains("Cash")) 
                {
                    // A simple check if they didn't pick cash, they should enter a phone number. But keeping it simple.
                }

                UIHelper.ShowToast("Payment Successful! Your order has been placed.");
                _cartItems.Clear();
                _cartCount = 0;
                _svcCartBtn.Text = "\ud83d\uded2  Cart (0)";
                _paymentOverlay.Visible = false;
            };
            dialog.Controls.Add(confBtn);
        }

        private void CreateContactSection()
        {
            contactPanel = new Panel { BackColor = Color.FromArgb(240, 242, 248), Name = "contactPanel" };
            mainScrollPanel.Controls.Add(contactPanel);

            // ── Blue header banner ─────────────────────────────────────────
            Panel banner = new Panel { BackColor = Color.FromArgb(37, 99, 235), Name = "ctBanner" };
            contactPanel.Controls.Add(banner);

            Label bannerTitle = new Label
            {
                Text = "Contact Us",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.White, AutoSize = true, Name = "ctBannerTitle"
            };
            banner.Controls.Add(bannerTitle);

            Label bannerSub = new Label
            {
                Text = "Have questions about our dining services? We'd love to hear from you.",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(219, 234, 254), AutoSize = true, Name = "ctBannerSub"
            };
            banner.Controls.Add(bannerSub);

            // ── White card ─────────────────────────────────────────────────
            Panel card = new Panel { BackColor = Color.White, Name = "ctCard" };
            card.Paint += RoundedPanel_Paint;
            contactPanel.Controls.Add(card);

            // Left form side
            string[] labels = { "Full Name", "Email", "Phone (e.g. 09/251234567)", "Location", "Message" };
            string[] pholders = { "Your full name", "Your email address", "Your Ethiopian phone number", "City, Region", "" };

            for (int i = 0; i < 5; i++)
            {
                Label lbl = new Label
                {
                    Text = labels[i],
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(55, 65, 81),
                    AutoSize = true,
                    Name = $"ctLbl{i}"
                };
                card.Controls.Add(lbl);

                if (i < 4) // Single-line fields
                {
                    TextBox tb = new TextBox
                    {
                        Font = new Font("Segoe UI", 11),
                        BackColor = Color.FromArgb(248, 250, 252),
                        BorderStyle = BorderStyle.FixedSingle,
                        ForeColor = Color.FromArgb(156, 163, 175),
                        Name = $"ctField{i}"
                    };
                    if (!string.IsNullOrEmpty(pholders[i]))
                    {
                        tb.Text = pholders[i];
                        tb.GotFocus  += (s, e) => { if (((TextBox)s).Text == ((TextBox)s).Tag?.ToString()) { ((TextBox)s).Text = ""; ((TextBox)s).ForeColor = Color.FromArgb(17,24,39); } };
                        tb.LostFocus += (s, e) => { if (string.IsNullOrEmpty(((TextBox)s).Text)) { ((TextBox)s).Text = ((TextBox)s).Tag?.ToString(); ((TextBox)s).ForeColor = Color.FromArgb(156,163,175); } };
                        tb.Tag = pholders[i];
                    }
                    card.Controls.Add(tb);
                }
                else // Message textarea
                {
                    TextBox msgBox = new TextBox
                    {
                        Multiline = true, ScrollBars = ScrollBars.Vertical,
                        Font = new Font("Segoe UI", 10),
                        BackColor = Color.FromArgb(248, 250, 252),
                        BorderStyle = BorderStyle.FixedSingle,
                        ForeColor = Color.FromArgb(156, 163, 175),
                        Text = "Please check the box above to enable",
                        Enabled = false,
                        Name = "ctMessage"
                    };
                    card.Controls.Add(msgBox);

                    // Note box
                    Panel noteBox = new Panel { BackColor = Color.FromArgb(219, 234, 254), Name = "ctNote" };
                    Label noteLbl = new Label
                    {
                        Text = "Note: Please check the box below to enable the message field",
                        Font = new Font("Segoe UI", 9),
                        ForeColor = Color.FromArgb(29, 78, 216),
                        AutoSize = true, Name = "ctNoteLbl"
                    };
                    noteBox.Controls.Add(noteLbl);
                    card.Controls.Add(noteBox);

                    // Checkbox to enable message
                    CheckBox chk = new CheckBox
                    {
                        Text = "Questions or Comments?",
                        Font = new Font("Segoe UI", 10),
                        ForeColor = Color.FromArgb(55, 65, 81),
                        AutoSize = true, Name = "ctChk"
                    };
                    chk.CheckedChanged += (s, ev) =>
                    {
                        msgBox.Enabled = chk.Checked;
                        if (chk.Checked) { msgBox.Text = ""; msgBox.ForeColor = Color.FromArgb(17,24,39); }
                        else { msgBox.Text = "Please check the box above to enable"; msgBox.ForeColor = Color.FromArgb(156,163,175); }
                    };
                    card.Controls.Add(chk);
                }
            }

            // Submit button
            Button submitBtn = new Button
            {
                Text = "Submit",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(37, 99, 235),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Name = "ctSubmit"
            };
            submitBtn.FlatAppearance.BorderSize = 0;
            submitBtn.Click += (s, e) => UIHelper.ShowToast("Message sent! We'll get back to you soon.");
            card.Controls.Add(submitBtn);

            // Right side — image
            string imgPath = System.IO.Path.Combine(Application.StartupPath, "img", "picdescktop.jpg");
            PictureBox contactPic = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.FromArgb(200, 210, 230),
                Name = "ctPic"
            };
            if (System.IO.File.Exists(imgPath))
                contactPic.Image = Image.FromFile(imgPath);
            contactPic.Paint += RoundedPanel_Paint;
            card.Controls.Add(contactPic);

            // Get in Touch section
            Label gitTitle = new Label
            {
                Text = "Get in Touch",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(37, 99, 235),
                AutoSize = true, Name = "ctGitTitle"
            };
            card.Controls.Add(gitTitle);

            Panel divLine = new Panel { BackColor = Color.FromArgb(219, 234, 254), Name = "ctDivLine" };
            card.Controls.Add(divLine);

            string[] infoLabels = { "Email:", "Phone:", "Address:" };
            string[] infoVals   = { "dining@dbu.edu.et", "+251 01 081 5440", "Debre Birhan University, Debre Birhan, Amhara, Ethiopia" };
            for (int i = 0; i < 3; i++)
            {
                Label lKey = new Label { Text = infoLabels[i], Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(55,65,81), AutoSize = true, Name = $"ctInfoK{i}" };
                Label lVal = new Label { Text = infoVals[i],   Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(37,99,235),  AutoSize = true, Name = $"ctInfoV{i}" };
                card.Controls.Add(lKey);
                card.Controls.Add(lVal);
            }

            LayoutContact();
        }

        private void LayoutContact()
        {
            if (contactPanel == null) return;
            int w = mainScrollPanel.ClientSize.Width;
            if (w <= 0) w = this.ClientSize.Width;

            contactPanel.Size = new Size(w, CONTACT_H);

            // Banner
            Panel banner = (Panel)contactPanel.Controls["ctBanner"];
            if (banner != null)
            {
                banner.Size = new Size(w, 110);
                banner.Location = new Point(0, 0);
                Control bt = banner.Controls["ctBannerTitle"];
                Control bs = banner.Controls["ctBannerSub"];
                if (bt != null) bt.Location = new Point((w - bt.Width) / 2, 22);
                if (bs != null) bs.Location = new Point((w - bs.Width) / 2, bt != null ? bt.Bottom + 6 : 60);
            }

            // Card
            Panel card = (Panel)contactPanel.Controls["ctCard"];
            if (card == null) return;

            int cardW = Math.Min(900, w - 80);
            int cardX = (w - cardW) / 2;
            card.Size     = new Size(cardW, CONTACT_H - 140);
            card.Location = new Point(cardX, 125);

            int leftW  = (int)(cardW * 0.52) - 30;   // form column width
            int rightX = leftW + 50;                   // right column start
            int rightW = cardW - rightX - 20;          // right column width
            int x0 = 20;                               // left margin

            // Layout form fields
            string[] fieldNames  = { "ctField0", "ctField1", "ctField2", "ctField3" };
            int[] topY = { 50, 110, 170, 230 };

            for (int i = 0; i < 4; i++)
            {
                Control lbl   = card.Controls[$"ctLbl{i}"];
                Control field = card.Controls[fieldNames[i]];
                if (lbl   != null) lbl.Location   = new Point(x0, topY[i] - 18);
                if (field != null) { field.Location = new Point(x0, topY[i]); ((TextBox)field).Size = new Size(leftW, 34); }
            }

            // Message label + textarea
            Control lblMsg = card.Controls["ctLbl4"];
            Control msg    = card.Controls["ctMessage"];
            Panel   note   = (Panel)card.Controls["ctNote"];
            Control chk    = card.Controls["ctChk"];
            Control sub    = card.Controls["ctSubmit"];

            if (lblMsg != null) lblMsg.Location = new Point(x0, 292);
            if (msg    != null) { msg.Location  = new Point(x0, 312); ((TextBox)msg).Size = new Size(leftW, 90); }
            if (note   != null)
            {
                note.Size     = new Size(leftW, 28);
                note.Location = new Point(x0, 408);
                Control nl = note.Controls["ctNoteLbl"];
                if (nl != null) nl.Location = new Point(8, 6);
            }
            if (chk != null) chk.Location = new Point(x0, 442);
            if (sub != null) { sub.Size = new Size(leftW, 42); sub.Location = new Point(x0, 480); }

            // Right side — image
            Control pic = card.Controls["ctPic"];
            if (pic != null) { pic.Size = new Size(rightW, 180); pic.Location = new Point(rightX, 20); }

            // Get in Touch
            Control git = card.Controls["ctGitTitle"];
            Panel   div = (Panel)card.Controls["ctDivLine"];
            if (git != null) git.Location = new Point(rightX, 218);
            if (div != null) { div.Size = new Size(rightW, 1); div.Location = new Point(rightX, git != null ? git.Bottom + 4 : 250); }

            int infoY = div != null ? div.Bottom + 12 : 260;
            for (int i = 0; i < 3; i++)
            {
                Control k = card.Controls[$"ctInfoK{i}"];
                Control v = card.Controls[$"ctInfoV{i}"];
                if (k != null) k.Location = new Point(rightX,              infoY);
                if (v != null) v.Location = new Point(rightX + 80,         infoY);
                infoY += (v != null) ? Math.Max(v.Height, 24) + 10 : 34;
            }
        }

        // ═══════════════════════════════════════════════════════
        // ROUNDED CORNERS (Paint handler)
        // ═══════════════════════════════════════════════════════
        private void RoundedPanel_Paint(object sender, PaintEventArgs e)
        {
            Control ctrl = (Control)sender;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int radius = 16;
            Rectangle rect = new Rectangle(0, 0, ctrl.Width, ctrl.Height);
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(rect.X,              rect.Y,               radius, radius, 180, 90);
                path.AddArc(rect.Right - radius, rect.Y,               radius, radius, 270, 90);
                path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius,   0, 90);
                path.AddArc(rect.X,              rect.Bottom - radius, radius, radius,  90, 90);
                path.CloseFigure();
                ctrl.Region = new Region(path);
            }
        }

        private void LandingForm_Load(object sender, EventArgs e) { }
    }
}
