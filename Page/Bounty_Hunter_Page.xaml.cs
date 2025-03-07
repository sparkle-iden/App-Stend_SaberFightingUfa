namespace MauiApp3.Page;

public partial class Bounty_Hunter_Page : ContentPage
{
    private string _userName;

    private Query_SQL _query_sql;
    public Bounty_Hunter_Page(string name)
	{
		InitializeComponent();
        _query_sql = new Query_SQL(name);
        _userName = name;
        Login_user.Text = name;
    }
    private void QR_scan(object sender, EventArgs e)
    {
        string Login = Login_user.Text;

        Navigation.PushAsync(new Page.Scan(Login));
    }
    private void Nazad(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await UpdateUserMoney(); // ���������� ������ � �������
        await UpdateQuestProgress();
    }
    private async Task UpdateQuestProgress()
    {
        try
        {
            var progress = await _query_sql.GetQuestProgress(_userName);

            // ��������� ������ ������� ������
            UpdateQuestStatus(Shoting_Progress, Shoting_Button, "���", progress);
            UpdateQuestStatus(Head_Hunter_Progress, Head_Hunter_Button, "����� �� ��������", progress);
            UpdateQuestStatus(Duel_Progress, Duel_Button, "�����", progress);
            UpdateQuestStatus(Fighting_Progress, Fightin_Button, "����������", progress);
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
}