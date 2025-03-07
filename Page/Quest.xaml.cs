using MySql.Data.MySqlClient;

namespace MauiApp3.Page;

public partial class Quest : ContentPage
{
    private string _userName;

    private Query_SQL _query_sql;
    public Quest(string name)
	{
		InitializeComponent();
        _query_sql = new Query_SQL(name);
        _userName = name;
        Login_user.Text = name;
    }
    // ��������������� ������ ��� ���������� ��� �������� �� ��������
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        UpdateUserMoney(); // ���������� ������ � �������
        UpdateQuestProgress();
      
        
    }
    private async Task UpdateQuestProgress()
    {
        try
        {
            var progress = await _query_sql.GetQuestProgress(_userName);

           
            
            UpdateQuestStatus(Progress_Archaaeology, excavations_Next, "����������", progress);
            UpdateQuestStatus(Progress_XenoZoo, Xeno_Zoo_Next, "�������������", progress);
            UpdateQuestStatus(Progress_Bounty_Hunter, Hunter_Next, "�������", progress);
            UpdateQuestStatus(Progress_PhotoZone, PhotoZone_Next, "����-����", progress);
            UpdateQuestStatus(Progress_Qustions, Quiz_game_Next, "���������", progress);
            UpdateQuestStatus(Progress_Archaaeology, excavations_Next, "����������", progress);
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

    // ����� ��� ��������� � ����������� ����� ������������
    private  async Task UpdateUserMoney()
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

    private void Next_excavations(object sender, EventArgs e)
    {      
        // ������� �� ��������� ��������
        Navigation.PushAsync(new Page.Archaeology(_userName));
    }

    private void Next_card_game(object sender, EventArgs e)
    { 
        // ������� �� ��������� ��������
        Navigation.PushAsync(new Page.Card_game(_userName));
    }

    private void Quiz_game(object sender, EventArgs e)
    {
        // ������� �� ��������� ��������
        Navigation.PushAsync(new Page.Quiz_Page(_userName));
    }

    private void Next_Xeno_Zoo(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page.XenoZoo(_userName));
    }

    private void Hunter(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page.Bounty_Hunter_Page(_userName));
    }

    private void PhotoZone(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page.PhotoZone_Page(_userName));
    }
}