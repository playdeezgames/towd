Imports towd.business

Friend Class MainMenuUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Const EMBARK_TEXT = "Embark!"
    Const SCUM_LOAD_TEXT = "Scum Load"
    Const LOAD_TEXT = "Load..."
    Const QUIT_TEXT = "Quit"

    Public Sub New(context As IUIContext(Of IWorld))
        Me.context = context
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean))) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)))(Array.Empty(Of (String, String, Boolean)))
    End Function

    Public Function GetChoices() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoices
        Return Task.FromResult(Of IEnumerable(Of String))({
                EMBARK_TEXT,
                SCUM_LOAD_TEXT,
                LOAD_TEXT,
                QUIT_TEXT
                })
    End Function

    Public Function GetPrompt() As String Implements IUIDialog.GetPrompt
        Return "Main Menu:"
    End Function

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Select Case choice
            Case EMBARK_TEXT
                context.World.Initialize()
                Return NeutralUIDialog.DetermineInPlayDialog(context)
            Case SCUM_LOAD_TEXT
                If context.LoadGame(SaveSlot.ScumSlot) Then
                    Return New MessageBoxUIDialog("Load Success!", {(Mood.Normal, $"You loaded {SaveSlot.ScumSlot.ToSaveSlotDescriptor.DisplayName}!", True)}, Function() NeutralUIDialog.DetermineInPlayDialog(context))
                End If
                Return New MessageBoxUIDialog("Load Failed!", {(Mood.Normal, $"Failed to load {SaveSlot.ScumSlot.ToSaveSlotDescriptor.DisplayName}!", True)}, Function() Me)
            Case LOAD_TEXT
                Return New LoadMenuUIDialog(context, Function() Me)
            Case QUIT_TEXT
                Return New ConfirmUIDialog("Are you sure you want to quit?", Function() Nothing, Function() Me)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
