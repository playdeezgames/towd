Imports towd.business

Friend Class GameMenuUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Const CONTINUE_TEXT = "Continue"
    Const SCUM_SAVE_GAME_TEXT = "Scum Save Game"
    Const SCUM_LOAD_GAME_TEXT = "Scum Load Game"
    Const SAVE_GAME_TEXT = "Save Game"
    Const ABANDON_GAME_TEXT = "Abandon Game"

    Public Sub New(context As IUIContext(Of IWorld))
        Me.context = context
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean))) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)))(Array.Empty(Of (String, String, Boolean)))
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Return Task.FromResult(Of IEnumerable(Of String))({
                CONTINUE_TEXT,
                SCUM_SAVE_GAME_TEXT,
                SCUM_LOAD_GAME_TEXT,
                SAVE_GAME_TEXT,
                ABANDON_GAME_TEXT
                })
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult("Game Menu")
    End Function

    Public Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Select Case choice
            Case CONTINUE_TEXT
                Return Task.FromResult(NeutralUIDialog.DetermineInPlayDialog(context))
            Case SCUM_LOAD_GAME_TEXT
                If context.LoadGame(SaveSlot.ScumSlot) Then
                    Return Task.FromResult(NeutralUIDialog.DetermineInPlayDialog(context))
                End If
                Return Task.FromResult(Of IUIDialog)(Me)
            Case SCUM_SAVE_GAME_TEXT
                context.SaveGame(SaveSlot.ScumSlot)
                Return Task.FromResult(Of IUIDialog)(Me)
            Case SAVE_GAME_TEXT
                Return Task.FromResult(Of IUIDialog)(New SaveGameUIDialog(context, Function() Me))
            Case ABANDON_GAME_TEXT
                Return Task.FromResult(Of IUIDialog)(New ConfirmUIDialog("Are you sure you want to abandon the game?", Function()
                                                                                                                           context.World.Abandon()
                                                                                                                           Return New MainMenuUIDialog(context)
                                                                                                                       End Function, Function() Me))
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
