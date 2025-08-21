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
            Dim parameters As New Dictionary(Of String, String)
            Dim data = context.GetParametersAsync().Result
            If data IsNot Nothing Then
                For Each parameter In data
                    parameters(parameter.Key) = AnsiConsole.Ask(Of String)($"[olive]{parameter.Key}[/]", parameter.Value)
                Next
            End If
            Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{context.GetPromptAsync().Result}[/]"}
            prompt.AddChoices(context.GetChoicesAsync().Result.ToArray())
            Dim choice = AnsiConsole.Prompt(prompt)
            context.Choose(choice, parameters).Wait()
        End While
    End Sub
End Module
