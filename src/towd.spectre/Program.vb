Imports Spectre.Console
Imports towd.business
Imports towd.ui

Module Program
    Sub Main(args As String())
        Console.Title = "TOWD"
        Dim context As IUIContext(Of IWorld) = New UIContext(Nothing)
        While Not context.IsClosed
            AnsiConsole.Clear()
            For Each line In context.Lines
                If line.Item3 Then
                    AnsiConsole.WriteLine(line.Item2)
                Else
                    AnsiConsole.Write(line.Item2)
                End If
            Next
            Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{context.Prompt}[/]"}
            prompt.AddChoices(context.Choices.ToArray())
            Dim choice = AnsiConsole.Prompt(prompt)
            context.Choose(choice)
        End While
    End Sub
End Module
