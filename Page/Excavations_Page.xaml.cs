namespace MauiApp3.Page;

public partial class Excavations_Page : ContentPage
{
    private Query_SQL _query_sql;
    private string _userName;
    public Excavations_Page(string name)
	{

        InitializeComponent();
        _userName = name;
        _query_sql = new Query_SQL(name);
        Login_user.Text = name;
        Quest_excavations.Text = "�������� ���������� ����� ����������� \r\n�������, � ������ �������� ������\r\n ���������, ������� ��� ���� ����� �����\r\n ��������� ����� � �������.";
        Quest_excavations1.Text = "������� ��������� ��� ��������� ��������� ����������";
        ;
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
            int userMoney = await _query_sql.GetMoneyUser(_userName);
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

    private void QR_scan(object sender, EventArgs e)
    {
        string Login = Login_user.Text;

        Navigation.PushAsync(new Page.Scan(Login));
    }
}