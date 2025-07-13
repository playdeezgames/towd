Friend Class MainMenuUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext
    Const EMBARK_TEXT = "Embark!"
    Const SCUM_LOAD_TEXT = "Scum Load"
    Const LOAD_TEXT = "Load..."
    Const QUIT_TEXT = "Quit"

    Public Sub New(context As IUIContext)
        Me.context = context
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IUIDialog.Lines
        Get
            Return Array.Empty(Of String)
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Return {
                EMBARK_TEXT,
                SCUM_LOAD_TEXT,
                LOAD_TEXT,
                QUIT_TEXT
                }
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return "Main Menu:"
        End Get
    End Property

    Public Function Choose(choice As String) As (String, IUIDialog) Implements IUIDialog.Choose
        Select Case choice
            Case EMBARK_TEXT
                context.World.Initialize()
                Return (GameState.Neutral, Nothing)
            Case SCUM_LOAD_TEXT
                If context.LoadGame(SaveSlot.ScumSlot) Then
                    Return (GameState.Neutral, Nothing)
                End If
                Return (Nothing, Me)
            Case LOAD_TEXT
                Return (Nothing, New LoadMenuUIDialog(context, Me))
            Case QUIT_TEXT
                Return (Nothing, New ConfirmUIDialog("Are you sure you want to quit?", Nothing, Me))
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
