Imports Spectre.Console
Imports towd.ui

Module Program
    Sub Main(args As String())
        Dim context As IUIContext = New UIContext
        context.Dialog = New SplashUIDialog(context)
        While context.Dialog IsNot Nothing
            AnsiConsole.Clear()
            For Each line In context.Dialog.Lines
                If line.Item3 Then
                    AnsiConsole.WriteLine(line.Item2)
                Else
                    AnsiConsole.Write(line.Item2)
                End If
            Next
            Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{context.Dialog.Prompt}[/]"}
            prompt.AddChoices(context.Dialog.Choices.ToArray())
            Dim choice = AnsiConsole.Prompt(prompt)
            context.Dialog = context.Dialog.Choose(choice)
        End While
    End Sub
End Module
