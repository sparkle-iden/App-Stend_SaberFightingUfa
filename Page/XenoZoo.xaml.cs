namespace MauiApp3.Page;

public partial class XenoZoo : ContentPage
{
    private Query_SQL _query_sql;
    private string _userName;
    private List<Question> _questions;
    private int complet;

    public XenoZoo(string name)
	{
		InitializeComponent();
          _userName = name;
        _query_sql = new Query_SQL(name);
        Login_user.Text = name;

        LoadQuestions();

        List<string> words = new List<string>
        {
            "����������",
            "������",
            "������-������",
            "��������������� ����",
            "�����",
            "�����",
            "��������"
        };

        // ������� ������ Random
        Random random = new Random();

        // ���������� ��������� ������
        int randomIndex = random.Next(words.Count);

        // �������� ��������� �����
        string randomWord = words[randomIndex];
        zoo.Text = randomWord;
            switch (randomWord)
        {
            case "����������":
                ShowQuestions(0, 2);
                break;
            case "������":
                ShowQuestions(3, 5);
                break;
            case "������-������":
                ShowQuestions(6, 8);
                break;
            case "��������������� ����":
                ShowQuestions(9, 11);
                break;
            case "�����":
                ShowQuestions(12, 14);
                break;
            case "�����":
                ShowQuestions(15, 17);
                break;
            case "��������":
                ShowQuestions(18, 20);
                break;
        }
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

    string questName = "�������������";

    private void Fail_Question(object sender, EventArgs e)
    {
        var button = sender as Button;
      
        if (button != null)
        {
            button.BackgroundColor = Colors.Red;  // ����� ������� ���� ��� ������������� ������
        }
        if (complet == 3)
        {
            Text_scroll.IsVisible = false;
            end.IsVisible = true;
            Button_scan.IsVisible = false;
            end.Text = "�� ��������� ��������� ������������";
            _query_sql.AddQuestProgressUser(_userName, questName);
        }

    }

    private async void Right_answer(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button != null)
        {
            button.BackgroundColor = Colors.Green;  // ����� ������ ���� ��� ����������� ������
        }
        int coinsToAdd = 1000; // ���������� ����� �� ���������� �����

        // ��������� ������ ������������ � ���� ������
        _query_sql.AddMoneyToUser(_userName, coinsToAdd);
        // ��������� ����������� ����� � ����������
        await UpdateUserMoney();
        if (complet == 3)
        {
            Text_scroll.IsVisible = false;
            end.IsVisible = true;
            end.Text = "�� ������� ������ ��������� ������������";
            Button_scan.IsVisible = false;
            _query_sql.AddQuestProgressUser(_userName, questName);
        }
    }


    private void ShowQuestions(int startIndex, int endIndex)
    {
        // �������� ������� �� �������� � ����������� �� ���������� ���������
        for (int i = startIndex; i <= endIndex; i++)
        {
            var question = _questions[i];
            string questionLabelName = $"qustions_{i - startIndex +1}";
            string answerButton1Name = $"question_{i - startIndex + 1}_answer_1";
            string answerButton2Name = $"question_{i - startIndex + 1}_answer_2";
            string answerButton3Name = $"question_{i - startIndex + 1}_answer_3";
            
            // ����������� ����� �������
            var questionLabel = this.FindByName<Label>(questionLabelName);
            Console.WriteLine($"������������ ������ � ��������: {i}");
            questionLabel.Text = question.Text;

            // ����������� ����� ��� ������ �������
            var button1 = this.FindByName<Button>(answerButton1Name);
            var button2 = this.FindByName<Button>(answerButton2Name);
            var button3 = this.FindByName<Button>(answerButton3Name);
            button1.Text = question.Answers[0];
            button2.Text = question.Answers[1];
            button3.Text = question.Answers[2];

            // ����������� ����������� � �������
            if (question.CorrectAnswerIndex == 0)
            {
                button1.Clicked += Right_answer;
            }
            else {
                button1.Clicked += Fail_Question;
            }
            if (question.CorrectAnswerIndex == 1)
            {
                button2.Clicked += Right_answer;
            }
            else {
                button2.Clicked += Fail_Question;
            }
            if (question.CorrectAnswerIndex == 2)
            {
                button3.Clicked += Right_answer;
            }else
            {
                button3.Clicked += Fail_Question;
            }
        }
    }
 

    private void LoadQuestions()
    {
        _questions = new List<Question>
    {
       
        new Question
        {
            Text = "������������� ����������� ������������",
            Answers = new List<string>
            {
                "����� ����� �������� �����, ������� ����� � �������� �� ����� � ������ ������� ������ ������ �����",
                "��� ����� ��������� ��������, �������� �� ���� ��������� �������.",
                "����� ������ �����, ��� � �������, � ������ �����, ��� � ������.",
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "�� ����� ������/�������/������� ���� �� ����� �����������?",
            Answers = new List<string>
            {
                "������",
                "������",
                "�����",
            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "��� ����� ���������� �����������?",
            Answers = new List<string>
            {
                "���������� ����������� �������, ��������� ���� � ������� ����������� ��������.",
                "����� ����� ����������� � �������� � ��������� ��������",
                "������������ ����� ���� � ������, ������� ����������� �� (��������, ����� ���������).",
            },
            CorrectAnswerIndex = 1
        },
        new Question
        {
            Text = "������� ��� � �������?",
            Answers = new List<string>
            {
                "��� ����",
                "������ ����",
                "��� ����",
            },
            CorrectAnswerIndex = 1
        },
        new Question
        {
            Text = "������� ��� ����� �������� ����� �������?",
            Answers = new List<string>
            {
                "5-6",
                "1-4",
                "������ 6",
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "��� ��� ��� �������� ������?",
            Answers = new List<string>
            {
                "���������",
                "������ ���� ��������",
                "�����, ������� � ������� �������",
            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "������ ����� ����?",
            Answers = new List<string>
            {
                "�����",
                "�����",
                "������-�������",

            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "������ �������?",
            Answers = new List<string>
            {
                "��� �����",
                "��� �����",
                "���� ������",
            },
            CorrectAnswerIndex = 1
        },
        new Question
        {
            Text = "�������� ������?",
            Answers = new List<string>
            {
                "��������������� �����",
                "�����",
                "��������� ����",
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "����� ����� ���� � ����� ��������?",
            Answers = new List<string>
            {
                "����",
                "�����",
                "�����"
            },
            CorrectAnswerIndex = 1
        },
        new Question
        {
            Text = "������ �����, ������� ���������� �� ������ ������?",
            Answers = new List<string>
            {
                "���� ����� ���������� �� ������ ���� � ���������� ������ ���������.",
                "���������� ���������� ������, ����� ���������� ������ ���������.",
                "���������� ������ ���� �� ���������� �� �����, ����� ������ ������."
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "� ����� ��������� �������?",
            Answers = new List<string>
            {
                "� ��������� ������� � ������� ����������.",
                "������ � �������",
                "� ����������� ����� � ������ ���������������."
            },
            CorrectAnswerIndex = 1
        },
        new Question
        {
            Text = "������� ������� �� ����� � ������?",
            Answers = new List<string>
            {
                "���� �������",
                "���� �����",
                "��� ������"
            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "��� �������������� ������?",
            Answers = new List<string>
            {
                "������� ��������",
                "����������� �� ������",
                "���������"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "���� ��������?",
            Answers = new List<string>
            {
                "������",
                "�����",
                "�����"
            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "��� �������� �����?",
            Answers = new List<string>
            {
                "�������� ������ � �������, ��������� ����������.",
                "������� �� ������ � ������� ������",
                "���������� ������ ���� ��� ��������� ������."
            },
            CorrectAnswerIndex = 1
        },
        new Question
        {
            Text = "���� � ���?",
            Answers = new List<string>
            {
                "150 ���������� � 3 ����� �����",
                "100 ���������� � 2.5 ����� �����",
                "150 ���������� � 4 ����� �����"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "�������� ����� ��������?",
            Answers = new List<string>
            {
                "����",
                "������",
                "������"
            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "���������� ����������� ����������?",
            Answers = new List<string>
            {
                "������ �����",
                "������ ������",
                "������ � �����"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "����������� ������� ���?",
            Answers = new List<string>
            {
                "�������� ������",
                "������",
                "���� �������"
            },
            CorrectAnswerIndex = 1
        },
        new Question
        {
            Text = "���������� ����� ������� ��������?",
            Answers = new List<string>
            {
                "����",
                "����������",
                "���",
            },
            CorrectAnswerIndex = 2
        },
       
    };
    }
    


    private void QR_scan(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page.Scan(_userName));
      
    }

    private void Visible_quest(object sender, EventArgs e)
    {
        Quest_rule.IsVisible = false;
        Button_start.IsVisible = false;
        Text_scroll.IsVisible = true;
        Button_scan.IsVisible = true;
    }
}