﻿Imports System.Text
Imports towd.business
Imports towd.ui

Friend Class DialogView
    Inherits View
    Private ReadOnly mainView As MainView
    Private ReadOnly promptLabel As Label
    Private ReadOnly linesTextView As TextView
    Private ReadOnly choicesListView As ListView
    Protected ReadOnly Property Context As IUIContext(Of IWorld)
        Get
            Return mainView.Context
        End Get
    End Property
    Sub New(mainView As MainView)
        Me.mainView = mainView
        Width = [Dim].Fill()
        Height = [Dim].Fill()
        promptLabel = New Label With
            {
                .Width = [Dim].Fill,
                .Text = "(dialog prompt)",
                .TextAlignment = TextAlignment.Centered
            }
        Add(promptLabel)
        linesTextView = New TextView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Percent(70),
                .Y = Pos.Bottom(promptLabel),
                .Enabled = False,
                .WordWrap = True,
                .Text = "(dialog lines go here)"
            }
        Add(linesTextView)
        choicesListView = New ListView With
            {
                .Width = [Dim].Fill,
                .Height = [Dim].Fill,
                .Y = Pos.Bottom(linesTextView)
            }
        AddHandler choicesListView.OpenSelectedItem, AddressOf OnChoicesListViewOpenSelectedItem
        Add(choicesListView)
    End Sub

    Private Sub OnChoicesListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Context.Choose(args.Value.ToString).Wait()
        UpdateView()
    End Sub

    Friend Sub UpdateView()
        If Context.IsClosed Then
            Application.RequestStop()
            Return
        End If
        promptLabel.Text = Context.GetPromptAsync().Result
        Dim builder As New StringBuilder
        For Each line In Context.GetLinesAsync().Result
            If line.EndsLine Then
                builder.AppendLine(line.Text)
            Else
                builder.Append(line.Text)
            End If
        Next
        linesTextView.Text = builder.ToString()
        linesTextView.ColorScheme = New ColorScheme With
                {
                    .Disabled = ColorScheme.Normal
                }
        choicesListView.SetSource(Context.GetChoicesAsync().Result.ToList)
    End Sub
End Class
