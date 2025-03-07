namespace MauiApp3.Page;

public partial class Quiz_Page : ContentPage
{
    private string _userName;
    private Query_SQL _query_sql;
    public Quiz_Page(string name)
	{
        InitializeComponent();
        _query_sql = new Query_SQL(name);
        Login_user.Text = name;
        _userName = name;

        LoadQuestions();
        DisplayQuestion();
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

    private void Fail_Question(object sender, EventArgs e)
    {
        count_qustions++;
        Fail_answer.IsVisible = true;

        Dispatcher.DispatchDelayed(TimeSpan.FromSeconds(2), () =>
        {
            Fail_answer.IsVisible = false;

            // ������� ������� ������
            _questions.RemoveAt(_currentQuestionIndex);

            if (_questions.Count > 0&&count_qustions<=4)
            {
                // ��������� � ���������� �������
                DisplayQuestion();
            }
            else
            {
                // ��������� ���������
                QuizEndLabel.IsVisible = true;
                QuestionLabel.IsVisible = false;
                Button1.IsVisible = false;
                Button2.IsVisible = false;
                Button3.IsVisible = false;
                Button4.IsVisible = false;
                strelka_pravo.IsVisible = true;
                strelka_vlevo.IsVisible = true;
                Nazad_Button.IsVisible = true;
                _query_sql.AddQuestProgressUser(_userName, questName);
            }
        });
       
    }
    private List<Question> _questions;
    private int _currentQuestionIndex;

    private void LoadQuestions()
    {
        _questions = new List<Question>
    {
        new Question
        {
            Text = "� ����� ����� ���������� ����������� �������� ������� ������?",
            Answers = new List<string>
            {
                "��� ���������� ����� ������ ���� �� �����, ������ ������������ ����� ����������� ��-������",
                "� ���������� �������, � ���������� �����������",
                "������-����� � �������-������� ���������",
                "������ ������� �� �������� �������"
            },
            CorrectAnswerIndex = 2
        },

        new Question
        {
            Text = "���� ��� ���� ��� ������ � ���� ������ � ��������� ��������, ������ ��� ������� ��� ����, ����� ��������������� ������������ ����� ��� ������� ������� �������������� ",
            Answers = new List<string>
            {
                "���������",
                "������",
                "����",
                "���������"
            },
            CorrectAnswerIndex = 1
        },
        new Question
        {
            Text = "������ �� ��� ������� ����� �������� ������� ���� ������� ���� ���������� � ��������",
            Answers = new List<string>
            {
                "��������",
                "������",
                "������",
                "������"
            },
            CorrectAnswerIndex = 3
        },
        new Question
        {
            Text = "�� ���� ������� ���������� ����� ���������, � ������� ��������� ����� � ����������� ����������� ������� ������������ ����. ��� �� ����� ������ �������� ��� ��� ���� ������, ������� ���������� � ����� �������, n������ ������ �� ������ �������.",
            Answers = new List<string>
            {
                "����",
                "�������",
                "���������",
                "�������"
            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "��������� ������� ��� �������, ������ �� ��� ������� �������� ��������� ��� ��������� ����� ��� ������ ���������� ��� ����� ����� ",
            Answers = new List<string>
            {
                "����",
                "�������",
                "�������",
                "���-����"
            },
            CorrectAnswerIndex = 0
        },

        new Question
        {
            Text = "������ ��� �� ��������� �������� ���� �������� 26 ���� 2003 ����, � �������� ������ ������ ��� � ������� ����� ��-���� ����.",
            Answers = new List<string>
            {
                "Star Wars Galaxies",
                "Star Wars The Old Republic",
                "Star Wars: Republic Heroes",
                "Star Wars Republic Commando"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "� ����� �� �������� ���� ���� �� ��������� �� ���� ������� ������ ���-9, ���� �� ���� ������������ ����� ��������� � �������� �������, ��� �� �������� � �������� �� ��������� ������� ������ ������ ��������� �� ��������� ���������� �� ������� �� ����, �� ������� �������. ",
            Answers = new List<string>
            {
                "Star Wars: Original Trilogy",
                "Star Wars: Galactic Battlegrounds",
                "Star Wars: Uprising",
                "Angry Birds Star Wars"
            },
            CorrectAnswerIndex = 1
        },
        
        new Question
        {
            Text = "���������� ��� ����� �������, �� ������, �������� ����������� ��������� � �������� �� ����������.",
            Answers = new List<string>
            {
                "������������ �����",
                "������ ������",
                "���-���",
                "����������� ���"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "�������������� �������� ���� ����� ������ ������� ��� ������ �������, �� ������� ��� ������ ��� ���������� ��������.",
            Answers = new List<string>
            {
                "������ 5. ������� ������� �������� ����",
                "������ 4. ����� �������",
                "������ 6. ����������� ������",
                "������ 3. ����� ������"
            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "� ���� ���� �� ����� ����������� ����� ����������, ��� �����, ������ ��� � ���� ������. � ���� ����� �������� � ���� �����-�����������, �������������� ��� ������-������, � ����� ������ �������.",
            Answers = new List<string>
            {
                "Star Wars: The Old Republic",
                "Star Wars: Jedi Academy",
                "Star Wars: Republic Commando",
                "Star Wars: Empire at war"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "��� ������� ���� ������� ���� ������, ����� ������������� �� ��� � � ���������� �� ��������. � � �������� ���������� �� ���������� �����, ������� ����������� ��� '��������, ���������'.",
            Answers = new List<string>
            {
                "��������",
                "��������",
                "����",
                "������"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "����������� ����� �� ������� ����, � ������� �� ������ �� �����-��������� ��-1138 �� �������� ����.",
            Answers = new List<string>
            {
                "Star Wars: Republic Commando",
                "Star Wars: Battlefront",
                "Star Wars: Jedi Outcast",
                "Star Wars: The Force Unleashed"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "����, ��������� �� ������ Quake III Arena, ��� �� ����� �������� � ���� �������� �����, ������� ������ �������� � �������� ����� ���������������� ����� ��������� ����.",
            Answers = new List<string>
            {
                "Star Wars: Jedi Knight: Jedi Academy",
                "Star Wars: The Old Republic",
                "Star Wars: Jedi Fallen Order",
                "Star Wars: Uprising"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "�� ����� ������ ���������� ������ ��������, ���� ���� ������ ������ ������ ����� ������ ���������, ����� �������� ���� ����� �������������, �� ��� ��������. ��� ������� ������������� ������ � ������� �������.",
            Answers = new List<string>
            {
                "�������� ����",
                "���� ������",
                "��� ����������",
                "��� ���������"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "������� ���, ���� � ������ � ���� ����������� �������� �������� �������� �� ��� �� ���� � ����.",
            Answers = new List<string>
            {
                "����������� ������� (��������)",
                "������",
                "�������",
                "������"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "� �������� ���������� � ������ ������ �������� ������ ������ ���� �������.",
            Answers = new List<string>
            {
                "�������",
                "������",
                "�����������",
                "�����"
            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "� ���� �������� ��������� ���� ��������� ������ ����� ����� �����-������� ��� ����� ������� ���.",
            Answers = new List<string>
            {
                "��-���-�����",
                "���� �����",
                "����-��� ����",
                "����"
            },
            CorrectAnswerIndex = 3
        },
        new Question
        {
            Text = "�� �������� ������ �������, �������� ������ �������, ���������� �������� ������� ���� � 1977 ����.",
            Answers = new List<string>
            {
                "���-��� ������",
                "����",
                "������ ���������",
                "��� ���������"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "�������� �������, �� �������� ���� ����� ������ ���� ������������� ��� ������� �BMF�.",
            Answers = new List<string>
            {
                "���-��� ������",
                "����-��� ����",
                "���� �����",
                "������ ���������"
            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "�������� �� ������, � ����� �������� ������ ���� ��������� �� � �����.",
            Answers = new List<string>
            {
                "��-���-�����",
                "����-��� ����",
                "���� �����",
                "����"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "��� ����� ��������� ���������, �������� ���������� ������ ������ ������� ����, �������� ���������� ��� ������.",
            Answers = new List<string>
            {
                "����",
                "�����",
                "�������",
                "���-��� �����"
            },
            CorrectAnswerIndex = 3
        },
        new Question
        {
            Text = "��� ������ ����� �� ��������� ������� ����� � ������. ��� ���� �������������� �����, ��� �� ������ ���� ������ ���������. �������� ������, � ������� ���� ��� �����.",
            Answers = new List<string>
            {
                "������ 3. ����� ������",
                "������ 1. ������� ������",
                "������ 6. ����������� ������",
                "������ 4. ����� �������"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "�������, ��� ��� ������ ����� ������ ������� ������� � ���� ����������-������� �� ���, �� ��� ��������� ���������, ��� ������� �� �� ������ ������ ����",
            Answers = new List<string>
            {
                "������ ����",
                "����� ����",
                "���� ����",
                "���� ������"
            },
            CorrectAnswerIndex = 3
        },
        new Question
        {
            Text = "����� ����, ��� ���� ��� ���� ���� ������� ����� ��� ������� � ����� ������� ����������, �� ������ � �������������, ����� ������� � ���� �������� ����, ��� �� �� ������, ��� � ���������� �� ������������ ����� ...., ��� ������, ��� � �������� ����, �������� ����������������",
            Answers = new List<string>
            {
                "20 000",
                "200 000",
                "10 000",
                "5 000"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "������������� ������������ ��������� ���� ���� ��, ��� �� � ��������� �������� ������ ����� ��������. ������ � �������� (��� ������ ������) ��������� �� ����� �������, ������� ��� ���������� ��������������� ����������� ������. � ����� �� ������ �� ������� �� ����������� � �������, �� ����� ������� �� �������.",
            Answers = new List<string>
            {
                "���������",
                "��������",
                "����������",
                "������"
            },
            CorrectAnswerIndex = 1
        },
        new Question
        {
            Text = "�� ����� �������, �� ������ ��-�� ���� ����������� ��������� �������� �-11, ��� ���������� � ������������ �������� - �����. ��� ��� �� �����������? ",
            Answers = new List<string>
            {
                "� ���� �������� ���� ������ ������",
                "���� ���������� �� �������",
                "������� ������ ���������� �����",
                "���������� ��������� ��������"
            },
            CorrectAnswerIndex = 2
        },
        new Question
        {
            Text = "� ���� Star Wars: Republic Commando ��������� ��������� ������� DC-17m ����� ������������������ � ����������,������� � ����������� ��������, ������ ��� ���� �� ���������� � ������� ������ �������� ��� ������������� ... ��� ���� ����������� ������ �������� ��� � �������? ",
            Answers = new List<string>
            {
                "��������",
                "�� ����� ������",
                "�� �����",
                "�� ���"
            },
            CorrectAnswerIndex = 0
        },
        new Question
        {
            Text = "���������� ������� ������ ��� ������� FN-2187 �� ��������� ����� � ����� ������ ������ ������, �������� ���, ������� �� ����� � ���������� � �������� ������� ",
            Answers = new List<string>
            {
                "�����",
                "����",
                "������",
                "���"
            },
            CorrectAnswerIndex = 1
        },
        new Question
        {
            Text = "������� �������� ����� ������� ���� ������� ��������, �������� ��� ���������� �����?",
            Answers = new List<string>
            {
                "������ 7",
                "������ 6",
                "������ 3",
                "������ 9"
            },
            CorrectAnswerIndex = 3
        },
        new Question
        {
            Text = "�� ���� ������� ����, ������ ���� � �� �� ����� ������ ������ ���������� - �� ���� ������ ������������, �� ��� ��������� ����� ������� ������ �� �������� �������� �����, � ��� ��-�� ����, ��� ��� ����� ���������� ..., ��������, ��� ���������� ��� ����� � ������� ������� ",
            Answers = new List<string>
            {
                "BB-8",
                "������ ���",
                "R2-D2",
                "Ÿ ������ ��������"
            },
            CorrectAnswerIndex = 0
        },

        new Question
        {
            Text = " �� ���� ������� ���� ������ ������� ���� ���� � ������� ���� ���� ��� ���������� ��������� ���������, ����� �� ������� ����� ���� ��������� ���������� ������ ������ � �����������. ",
            Answers = new List<string>
            {
                "�������",
                "�������",
                "������� III",
                "������"
            },
            CorrectAnswerIndex = 0
        },
    };
    }
    int count_qustions;
    private void DisplayQuestion()
    {
        if (_questions.Count == 0)
            return;

        var random = new Random();
        _currentQuestionIndex = random.Next(_questions.Count);
        var question = _questions[_currentQuestionIndex];

        // ��������� ����� �������
        QuestionLabel.Text = question.Text;

        // ������������� ����� ������
        Button1.Text = question.Answers[0];
        Button2.Text = question.Answers[1];
        Button3.Text = question.Answers[2];
        Button4.Text = question.Answers[3];

        // ���������� ���������� �����������
        Button1.Clicked -= HandleAnswerClick;
        Button2.Clicked -= HandleAnswerClick;
        Button3.Clicked -= HandleAnswerClick;
        Button4.Clicked -= HandleAnswerClick;

        // ����������� ����������� � �������
        Button1.Clicked += HandleAnswerClick;
        Button2.Clicked += HandleAnswerClick;
        Button3.Clicked += HandleAnswerClick;
        Button4.Clicked += HandleAnswerClick;

        // �������� ���������� ������
        Button1.ClassId = "0";
        Button2.ClassId = "1";
        Button3.ClassId = "2";
        Button4.ClassId = "3";
    }
    private void HandleAnswerClick(object sender, EventArgs e)
    {
        if (sender is Button clickedButton)
        {
            int selectedAnswerIndex = int.Parse(clickedButton.ClassId);

            // ���������, ���������� �� ��� �����
            if (selectedAnswerIndex == _questions[_currentQuestionIndex].CorrectAnswerIndex)
            {
                Right_answer(sender, e);
            }
            else
            {
                Fail_Question(sender, e);
            }
        }
    }

    string questName = "���������";

    private void Right_answer(object sender, EventArgs e)
    {
        int coinsToAdd = 1000; // ���������� ����� �� ���������� �����
        count_qustions++;
        // ��������� ������ ������������ � ���� ������
        _query_sql.AddMoneyToUser(_userName, coinsToAdd);

        // ��������� ����������� ����� � ����������
        UpdateUserMoney();

        // ������� ������� ������ � ���������� ���������
        _questions.RemoveAt(_currentQuestionIndex);

        if (_questions.Count > 0 && count_qustions <= 4)
        {
            DisplayQuestion();
        }
        else
        {
            // ��������� ���������
            QuizEndLabel.IsVisible = true;
            QuestionLabel.IsVisible = false;
            Button1.IsVisible = false;
            Button2.IsVisible = false;
            Button3.IsVisible = false;
            Button4.IsVisible = false;
            strelka_pravo.IsVisible = true;
            strelka_vlevo.IsVisible = true;
            Nazad_Button.IsVisible = true;
            _query_sql.AddQuestProgressUser(_userName, questName);
        }
       
    }

}