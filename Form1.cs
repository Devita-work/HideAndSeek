using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace дом_стр_322
{
    public partial class Form1 : Form
    {
        abstract class Location
        {
            public Location(string name)
            {
                this.name = name;
            }

            public virtual bool IsLocked
            {
                get { return false; }
            }

            public Location[] Exits;
            private string name;
            public string Name
            {
                get { return name; }
            }
            public virtual string Description
            {
                get
                {
                    string description = "Вы находитесь в " + name + ". Вы видите двери, ведущие в: ";
                    for (int i = 0; i < Exits.Length; i++)
                    {
                        description += " " + Exits[i].Name;
                        if (i != Exits.Length - 1)
                            description += ",";
                    }
                    description += ".";
                    return description;
                }
                set { string description = value; }
            }
        }

        interface IHasExteriorDoor
        {
            string DoorDescription { get; }
            Location DoorLocation { get; set; }
        }

        class Attic : Location
        {
            private string decoration;
            public Attic(string name, string decoration, bool islocked) : base(name)
            {
                this.decoration = decoration;
            }

            public override string Description
            {
                get
                {
                    return base.Description + " Вы видите " + decoration + ".";
                }
            }
        }

        class Room : Location
        {
            private string decoration;
            public Room(string name, string decoration) : base(name)
            {
                this.decoration = decoration;
            }

            public override string Description
            {
                get
                {
                    return base.Description + " Вы видите " + decoration + ".";
                }
            }
        }

        interface IHidingPlace
        {
            string HidingPlaceName { get; }
        }

        class RoomWithHidingPlace : Room, IHidingPlace
        {
            public RoomWithHidingPlace(string name, string decoration, string hidingPlaceName) : base(name, decoration)
            {
                this.hidingPlaceName = hidingPlaceName;
            }
            private string hidingPlaceName;
            public string HidingPlaceName
            {
                get { return hidingPlaceName; }
            }

            public override string Description
            {
                get { return base.Description + " Спрятаться можно " + hidingPlaceName + "."; }
            }
        }

        class RoomWithDoor : RoomWithHidingPlace, IHasExteriorDoor
        {
            public RoomWithDoor(string name, string decoration, string hidingPlaceName, string doorDescription) : base(name, decoration, hidingPlaceName)
            {
                this.doorDescription = doorDescription;
            }

            private string doorDescription;
            public string DoorDescription
            {
                get { return doorDescription; }
            }
            private Location doorLocation;
            public Location DoorLocation
            {
                get { return doorLocation; }
                set { doorLocation = value; }
            }

        }     

        class WindowAttic : Location
        {
            private string name;
            private string description;
            public WindowAttic (string name, string description) : base (name)
            {
                this.name = name;
                this.description = description;
            }

            public override string Description
            {
                get
                {
                    return "Убегая от монстра вы вылетели через окно на чердаке, если бы не дерево, растущее на заднем дворе вы бы точно разбились. " + "Единственный путь спуститься по дереву на задний двор";
                }


            }
        }

        class Outside : Location
        {
            private bool hot;
            public bool Hot { get { return hot; } }

            public Outside(string name, bool hot) : base(name)
            {
                this.hot = hot;
            }
            public override string Description
            {
                get
                {
                    string NewDescription = base.Description;
                    if (hot)
                        NewDescription += " Очень жарко.";
                    return NewDescription;
                }
                set { base.Description = value; }
            }
        }

        class OutsideWithHidingPlace : Outside, IHidingPlace
        {
            public OutsideWithHidingPlace(string name, bool hot, string hidingPlaceName):base(name, hot)
            {
                this.hidingPlaceName = hidingPlaceName;
            }
            private string hidingPlaceName;
            public string HidingPlaceName
            {
                get { return hidingPlaceName; }
            }
            public override string Description
            {
                get { return base.Description + " Можно спрятаться " + hidingPlaceName + "."; }
            }
        }

        class Opponent
        {
            private Random random;
            private Location myLocation;
            public Opponent(Location startingLocation)
            {
                myLocation = startingLocation;
                random = new Random();
            }
            public void Move()
            {
                if (myLocation is IHasExteriorDoor)
                {
                    IHasExteriorDoor LocationWithDoor = myLocation as IHasExteriorDoor;
                    if (random.Next(2) == 1)
                        myLocation = LocationWithDoor.DoorLocation;
                }
                bool hidden = false;
                while (!hidden)
                {
                    int rand = random.Next(myLocation.Exits.Length);
                    myLocation = myLocation.Exits[rand];
                    if (myLocation is IHidingPlace)
                        hidden = true;
                }
            }
            public bool Check(Location locationToCheck)
            {
                if (locationToCheck != myLocation)
                    return false;
                else
                    return true;
            }
        }
        class OutsideWithDoor : Outside, IHasExteriorDoor
        {
            public OutsideWithDoor(string name, bool hot, string doorDescription) : base(name, hot)
            {
                this.doorDescription = doorDescription;
            }
            private string doorDescription;
            public string DoorDescription
            {
                get { return doorDescription; }
            }
            private Location doorLocation;
            public Location DoorLocation
            {
                get { return doorLocation; }
                set { doorLocation = value; }
            }
            public override string Description
            {
                get { return base.Description + " Вы видите " + doorDescription + "."; }
                set { base.Description = value; }
            }
        }
        public Form1()
        {
            InitializeComponent();
            CreateObjects();
            opponent = new Opponent(frontYard);
            ResetGame(false);
        }
        int Moves;
        Location currentLocation;
        RoomWithDoor livingRoom;
        RoomWithHidingPlace diningRoom;
        RoomWithDoor kitchen;
        Room stairs;
        RoomWithHidingPlace hallway;
        RoomWithHidingPlace bathroom;
        RoomWithHidingPlace masterBedroom;
        RoomWithHidingPlace secindBedroom;

        WindowAttic window;
        OutsideWithDoor frontYard;
        OutsideWithDoor backYard;
        OutsideWithHidingPlace garden;
        OutsideWithHidingPlace driveway;

        Attic attic;

        Opponent opponent;        

        private void MoveToANewLocation(Location newlocation)
        {
            Moves++;
            if ((currentLocation == attic) & (newlocation == livingRoom)) {
                MessageBox.Show("Кто-то закрыл за вами дверь в гостинную! Ищи другой способ выбраться!");
            }
            else {
                currentLocation = newlocation;
                RedrawForm();
            }
        }

        private void RedrawForm()
        {
            exist.Items.Clear();
            for (int i = 0; i < currentLocation.Exits.Length; i++)
                exist.Items.Add(currentLocation.Exits[i].Name);
            exist.SelectedIndex = 0;
            description.Text = currentLocation.Description + "\r\n(перемещение #" + Moves + ")";
            if (currentLocation is IHidingPlace)
            {
                IHidingPlace hidingPlace = currentLocation as IHidingPlace;
                check.Text = "Check " + hidingPlace.HidingPlaceName;
                check.Visible = true;
            }
            else
            {
                check.Visible = false;
                check.Text = "";

            }
            if (currentLocation is IHasExteriorDoor)
                goThroughtTheDoor.Visible = true;
            else
                goThroughtTheDoor.Visible = false;
            if (currentLocation == attic)
            {
                
                timer1.Stop();
                timer1.Interval = 5000;
                timer1.Enabled = true;
                MessageBox.Show("На чердаке жуткий монстр! Еще немного и он вас съест!");
                timer1.Start();
            }
            else
            {
                timer1.Enabled = false;
            }
        }

        private void CreateObjects()
        {
            livingRoom = new RoomWithDoor("Гостиная", "старинный ковер","в гардеробе", "дубовая дверь с латунной ручкой");
            diningRoom = new RoomWithHidingPlace("Столовая", "хрустальная люстра", "в высоком шкафу");
            kitchen = new RoomWithDoor("Кухня", "плита из нержавеющей стали", "в сундуке", "сетчатая дверь");
            stairs = new Room("Лестница", "деревянные перила");
            hallway = new RoomWithHidingPlace("Верхний коридор", "картина с собакой", "в гардеробе");
            bathroom = new RoomWithHidingPlace("Ванная", "раковина и туалет", "в душе");
            masterBedroom = new RoomWithHidingPlace("Главная спальня", "большая кровать", "под кроватью");
            secindBedroom = new RoomWithHidingPlace("Вторая спальня", "маленькая кровать", "под кроватью");
            frontYard = new OutsideWithDoor("лужайка", false, " тяжелая дубовая дверь");
            backYard = new OutsideWithDoor("Задний двор", true, "сетчатая дверь");
            window = new WindowAttic("Окно", "Убегая от монстра вы вылетели через окно на чердаке, если бы не дерево, растущее на заднем дворе вы бы точно разбились.");
            garden = new OutsideWithHidingPlace("Сад", false, "в сарае");
            driveway = new OutsideWithHidingPlace("Подъезд", true, "в гараже");

            attic = new Attic("Чердак", "Слабо освещен приоткрытым окном", false);

            window.Exits = new Location[] { backYard };
            attic.Exits = new Location[] { window, livingRoom};
            diningRoom.Exits = new Location[] { livingRoom, kitchen };
            livingRoom.Exits = new Location[] { diningRoom, stairs, attic };
            kitchen.Exits = new Location[] { diningRoom};
            stairs.Exits = new Location[] { livingRoom, hallway };
            hallway.Exits = new Location[] { stairs, bathroom, masterBedroom, secindBedroom };
            bathroom.Exits = new Location[] {hallway};
            masterBedroom.Exits = new Location[] { hallway };
            secindBedroom.Exits = new Location[] { hallway };
            frontYard.Exits = new Location[] { backYard, garden, driveway};
            backYard.Exits = new Location[] { frontYard, garden, driveway };
            garden.Exits = new Location[] { backYard, frontYard };
            driveway.Exits = new Location[] { backYard, frontYard };

            livingRoom.DoorLocation = frontYard;
            frontYard.DoorLocation = livingRoom;
            kitchen.DoorLocation = backYard;
            backYard.DoorLocation = kitchen;
        }

        private void ResetGame (bool displayMessage)
        {
            if (displayMessage)
            {
                MessageBox.Show("Меня нашли за " + Moves + " ходов!");
                IHidingPlace foundLocation = currentLocation as IHidingPlace;
                description.Text = "Соперник найден за " + Moves + " ходов! Он прятался в " + foundLocation.HidingPlaceName + '.';
            }
            Moves = 0;
            Hide.Visible = true;
            goHere.Visible = false;
            check.Visible = false;
            goThroughtTheDoor.Visible = false;
            exist.Visible = false;
        }

        private void Reset(bool message)
        {
            if(message)
            {
                MessageBox.Show("Вы проиграли! Вас съел монстр");
            }
            Moves = 0;
            Hide.Visible = true;
            goHere.Visible = false;
            check.Visible = false;
            goThroughtTheDoor.Visible = false;
            exist.Visible = false;
        }

        private void goHere_Click(object sender, EventArgs e)
        {
            MoveToANewLocation(currentLocation.Exits[exist.SelectedIndex]);
        }

        private void goThroughtTheDoor_Click(object sender, EventArgs e)
        {
            IHasExteriorDoor hasDoor = currentLocation as IHasExteriorDoor;
            MoveToANewLocation(hasDoor.DoorLocation);
        }

        private void check_Click(object sender, EventArgs e)
        {
            Moves++;
            if (opponent.Check(currentLocation))
                ResetGame(true);
            else
                RedrawForm();
        }

        private void Hide_Click(object sender, EventArgs e)
        {
            Hide.Visible = false;
            for (int i = 0; i <= 10; i++)
            {
                opponent.Move();
                description.Text = i + "... ";
                Application.DoEvents();
                System.Threading.Thread.Sleep(200);
            }
            description.Text = "Я иду искать!";
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);
            goHere.Visible = true;
            exist.Visible = true;
            MoveToANewLocation(livingRoom);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            Reset(true);
            currentLocation = null;
        }

        private void exist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
