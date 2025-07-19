Imports Spectre.Console
Imports towd.business
Imports towd.ui

Module Program
    Sub Main(args As String())
        Console.Title = "TOWD"
        Dim context As IUIContext(Of IWorld) = New UIContext(New Persister)
        While Not context.IsClosed
            AnsiConsole.Clear()
            For Each line In context.GetLinesAsync().Result
                If line.EndsLine Then
                    AnsiConsole.WriteLine(line.Text)
                Else
                    AnsiConsole.Write(line.Text)
                End If
            Next
            Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{context.GetPrompt()}[/]"}
            prompt.AddChoices(context.GetChoices().ToArray())
            Dim choice = AnsiConsole.Prompt(prompt)
            context.Choose(choice)
        End While
    End Sub
End Module
