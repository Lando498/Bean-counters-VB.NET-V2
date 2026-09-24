Imports System.ComponentModel
Imports System.IO
Imports System.Security
Imports Microsoft.VisualBasic.Devices

Public Class Form1
    Dim ingame = False
    Dim score = 0
    Dim speed = 2
    Dim lives = 3
    Dim highscore As Integer = 0
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button1.Visible = False
        PictureBox1.Visible = True
        PictureBox2.Visible = True
        ingame = True
        spawnsack()
    End Sub

    Function spawnsack()
        Dim rnd As New Random()
        Dim randomx As Integer = rnd.Next(-2, 400)
        PictureBox2.Location = New Point(randomx, -68)
        Return randomx
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If ingame = True Then
            PictureBox1.Location = New Point(MousePosition.X - 150, PictureBox1.Location.Y)
            If PictureBox2.Location.X < PictureBox1.Location.X + 128 And PictureBox2.Location.X > PictureBox1.Location.X - 20 And PictureBox2.Location.Y > 140 Then
                spawnsack()
                score += 1
                speed += 1
                If score > highscore Then
                    highscore = score
                    Label6.Text = highscore
                End If
                Label1.Text = score.ToString()
                Label4.Text = lives.ToString()
            End If
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If ingame = True Then
            If speed > 50 Then
                PictureBox2.Location = New Point(PictureBox2.Location.X, PictureBox2.Location.Y + 50)
            Else
                PictureBox2.Location = New Point(PictureBox2.Location.X, PictureBox2.Location.Y + speed)
            End If

            If PictureBox2.Location.Y > 400 Then
                lives -= 1
                Label4.Text = lives.ToString()
                spawnsack()
            End If
        End If
        If lives < 0 Then
            If score > highscore Then
                highscore = score
                Label6.Text = highscore
            End If
            ingame = False
            PictureBox1.Visible = False
            PictureBox2.Visible = False
            Button1.Visible = True
            lives = 3
            speed = 2
            score = 0
            Label1.Text = score.ToString()
            Label4.Text = lives.ToString()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        spawnsack()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = score.ToString()
        Label4.Text = lives.ToString()
        If File.Exists("highscore.txt") Then
            Dim highstring As String
            highstring = File.ReadAllText("highscore.txt")
            highscore = Convert.ToInt32(highstring)
        Else File.Create("highscore.txt").Dispose()
        End If
        Label6.Text = highscore.ToString()
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If File.Exists("highscore.txt") Then
            File.WriteAllText("highscore.txt", highscore.ToString())
        Else File.Create("highscore.txt").Dispose()
            File.WriteAllText("highscore.txt", highscore.ToString())
        End If
    End Sub
End Class
