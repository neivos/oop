

using System.Text;

internal class Info
{
    private string hoten, lop, username, dcemail;
    private int masv;
    public Info()
    {
        this.hoten = string.Empty;
        this.lop = string.Empty;
        this.username = string.Empty;
        this.dcemail = string.Empty;
        this.masv = 0;
    }
    public Info(string hoten , string lop , string username , string dcemail, int masv)
    {
        this.hoten = hoten;
        this.lop = lop;
        this.username = username;
        this.dcemail = dcemail;
        this.masv = masv;
    }
    public void Show()
    {
        Console.WriteLine($"Ho ten: {this.hoten}\tLop: {this.lop}\tMa sinh vien: {this.masv}\tGit Username: {this.username}\tDia chi email:{this.dcemail}");
        Console.ReadKey();
    }
        
    
        
      
}

internal class Program
{

    static void Main(string[] args)
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;
        Info i = new Info("Trịnh Phúc Anh", "12423TN", "neivos", "trinhphucanh2005@gmail.com",12423003);
        i.Show();
        Console.ReadKey();
    }
}