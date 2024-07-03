public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class Admin
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Notification> Notifications { get; set; } = new List<Notification>();
}

public class Post
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreationDateTime { get; set; }
    public int LikeCount { get; set; }
    public int ViewCount { get; set; }
}

public class Notification
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime DateTime { get; set; }
    public User FromUser { get; set; }
    public Admin ToAdmin { get; set; }
}

class Program
{
    static List<User> users = new List<User>();
    static List<Admin> admins = new List<Admin>();
    static List<Post> posts = new List<Post>();
    static List<Notification> notifications = new List<Notification>();

    static void Main()
    {
        InitializeData();
        Console.WriteLine("User Or Admin  (U/A): ");
        string choice = Console.ReadLine()!;

        if (choice == "U")
        {
            UserLogin();
        }
        else if (choice == "A")
        {
            AdminLogin();
        }
        else
        {
            Console.WriteLine("Wrong choice!");
        }
    }

    static void InitializeData()
    {
        admins.Add(new Admin { Id = 1, Username = "Admin", Email = "admin@gmail.com", Password = "admin" });
        users.Add(new User { Id = 1, Name = "User1", Surname = "Surname1", Age = 25, Email = "user1@gmail.com", Password = "user1" });
        posts.Add(new Post { Id = 1, Content = "Porsche 911 Turbo Brabus", CreationDateTime = DateTime.Now, LikeCount = 0, ViewCount = 0 });
    }

    static void UserLogin()
    {
        Console.WriteLine("Email: ");
        string email = Console.ReadLine()!;
        Console.WriteLine("Password: ");
        string password = Console.ReadLine()!;

        User user = null;
        foreach (var u in users)
        {
            if (u.Email == email && u.Password == password)
            {
                user = u;
                break;
            }
        }

        if (user != null)
        {
            UserMenu(user);
        }
        else
        {
            Console.WriteLine("Wrong email or password!");
        }
    }

    static void AdminLogin()
    {
        Console.WriteLine("Username: ");
        string username = Console.ReadLine()!;
        Console.WriteLine("Password: ");
        string password = Console.ReadLine()!;

        Admin admin = null;
        foreach (var a in admins)
        {
            if (a.Username == username && a.Password == password)
            {
                admin = a;
                break;
            }
        }

        if (admin != null)
        {
            AdminMenu(admin);
        }
        else
        {
            Console.WriteLine("Wrong username or password!");
        }
    }

    static void UserMenu(User user)
    {
        while (true)
        {
            Console.WriteLine("1. View posts");
            Console.WriteLine("2. Like post");
            Console.WriteLine("0. Back");

            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
            {
                ViewPosts(user);
            }
            else if (choice == 2)
            {
                LikePost(user);
            }
            else if (choice == 0)
            {
                Main();
                return;
            }
        }
    }

    static void AdminMenu(Admin admin)
    {
        while (true)
        {
            Console.WriteLine("1. View Notifications");
            Console.WriteLine("0. Back");

            int choice = Convert.ToInt32(Console.ReadLine())!;

            if (choice == 1)
            {
                ViewNotifications(admin);
            }
            else if (choice == 0)
            {
                Main();
                return;
            }
        }
    }

    static void ViewPosts(User user)
    {
        foreach (var post in posts)
        {
            Console.WriteLine($"ID: {post.Id}, Content: {post.Content}, Likes: {post.LikeCount}, Views: {post.ViewCount}");
        }

        Console.WriteLine("Enter your ID to check the mail.: ");
        int id = Convert.ToInt32(Console.ReadLine())!;

        Post postToView = null;
        foreach (var post in posts)
        {
            if (post.Id == id)
            {
                postToView = post;
                break;
            }
        }

        if (postToView != null)
        {
            postToView.ViewCount++;
            CreateNotification(admins[0], user, postToView, "Post viewed!");
        }
    }

    static void LikePost(User user)
    {
        Console.WriteLine("Enter the post ID to like.: ");
        int id = Convert.ToInt32(Console.ReadLine())!;

        Post postToLike = null;
        foreach (var post in posts)
        {
            if (post.Id == id)
            {
                postToLike = post;
                break;
            }
        }

        if (postToLike != null)
        {
            postToLike.LikeCount++;
            CreateNotification(admins[0], user, postToLike, "Post liked");
        }
    }

    static void ViewNotifications(Admin admin)
    {
        foreach (var notification in notifications)
        {
            if (notification.ToAdmin.Id == admin.Id)
            {
                Console.WriteLine($"ID: {notification.Id}, Text: {notification.Text}, DateTime: {notification.DateTime}");
            }
        }
    }

    static void CreateNotification(Admin admin, User user, Post post, string action)
    {
        Notification notification = new Notification
        {
            Id = notifications.Count + 1,
            Text = $"{user.Name} {user.Surname} with {action}: {post.Content}",
            DateTime = DateTime.Now,
            FromUser = user,
            ToAdmin = admin
        };
        notifications.Add(notification);
    }
}