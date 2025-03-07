using MySql.Data.MySqlClient;

namespace MauiApp3.Page;

public partial class encryption_tabel : ContentPage
{
    private Query_SQL _query_sql;
    string server_login;
    public encryption_tabel(string name)
	{
		InitializeComponent();
        _query_sql = new Query_SQL(name);
        Login_user.Text = name;
        server_login = name;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await UpdateUserMoney(); // ���������� ������ � �������
    }

    // ����� ��� ��������� � ����������� ����� ������������
    private async Task UpdateUserMoney()
    {
        try
        {
            // ��������� ����� ������������ ����������
            int userMoney = await _query_sql.GetMoneyUser(server_login);
            User_Money.Text = userMoney.ToString();
        }
        catch (Exception ex)
        {
            // ����������� ������
            await DisplayAlert("������", $"�� ������� �������� ������ ������������: {ex.Message}", "OK");
        }
    }
    private void Nazad(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void Answer(object sender, EventArgs e)
    {
        string answer = "�����";
        if(Query_answer.Text == answer){
            Answer_right.IsVisible =true;
            string questName = "����������� ��������";
            _query_sql.AddQuestProgressUser(server_login, questName);

            // ��������� �������� � �������� ������
            MainThread.BeginInvokeOnMainThread(() =>
                {
                    
                    // ��������� ������ � ���� ������
                    string connectionString = "Server=server269.hosting.reg.ru;Database=u2917647_Sparkle;User ID=u2917647_default;Password=1tB6J7OD3cmt3JD1;Charset=utf8mb4;";  
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = "UPDATE User SET UserMoney = UserMoney + @coinsToAdd WHERE UserName = @name";
                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@coinsToAdd", 2000);
                            command.Parameters.AddWithValue("@name", server_login);
                            _query_sql.AddQuestProgressUser(server_login, "����������");
                            command.ExecuteNonQuery();
                          
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("������ �����������: " + ex.Message);
                        }
                    }
                });

            
        }
      
       
     
    }
}