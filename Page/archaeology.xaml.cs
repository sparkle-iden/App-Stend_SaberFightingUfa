using MySql.Data.MySqlClient;

namespace MauiApp3.Page;

public partial class Archaeology : ContentPage
{
    private string _userName;
    private Query_SQL _query_sql;
    public Archaeology(string name)
	{
        InitializeComponent();

        _query_sql = new Query_SQL(name);
        Login_user.Text = name;
        _userName = name;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await UpdateUserMoney(); // ���������� ������ � �������
        await UpdateQuestProgress();
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

    private async Task UpdateQuestProgress()
    {
        try
        {
            var progress = await _query_sql.GetQuestProgress(_userName);

            // ��������� ������ ������� ������
            UpdateQuestStatus(Lost_Manuscript_Label_Progress, Lost_Manuscript_Button, "����� ���������", progress);
            UpdateQuestStatus(Encryotion_Label_Progress, Encryotion_Button, "����������� ��������", progress);
            UpdateQuestStatus(Lost_Artefact_Label_Progress, Lost_Artefact_Button, "����� ����������", progress);
            UpdateQuestStatus(Excavations_Label_Progress, Excavations_Button, "��������", progress);
        }
        catch (Exception ex)
        {
            await DisplayAlert("������", $"�� ������� ��������� �������� �������: {ex.Message}", "OK");
        }
        
       
    }
  

    private void UpdateQuestStatus(Label progressLabel, ImageButton questButton, string questName, Dictionary<string, (int current, int max)> progress)
    {
        if (progress.ContainsKey(questName))
        {
            var (current, max) = progress[questName];
            progressLabel.Text = $"{current}/{max}";
            questButton.IsEnabled = current < max; // ��������� ������, ���� ����� ��������
        }
        else
        {
            progressLabel.Text = "0/0";
            questButton.IsEnabled = false; // ��������� ������, ���� ������ ���
        }
    }

   

    private void Nazad(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void Excavations(object sender, EventArgs e)
    {
        string Login = Login_user.Text;
    
        Navigation.PushAsync(new Page.Excavations_Page(Login));
    }

    private void Holocron(object sender, EventArgs e)
    {
        string Login = Login_user.Text;
       
        Navigation.PushAsync(new Page.Holocron_Search(Login));
    }

    private void Encryprion(object sender, EventArgs e)
    {
        string Login = Login_user.Text;
        
        Navigation.PushAsync(new Page.encryption_tabel(Login));
    }

    private void Lost_manusctipt(object sender, EventArgs e)
    {
        string Login = Login_user.Text;
       
        Navigation.PushAsync(new Page.Manuscripts_lost(Login));
    }
}