Imports towd.business

Friend Class VerbMenuUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Const AVAILABLE_TEXT = "Available"
    Const ALL_TEXT = "All"

    Public ReadOnly Property Lines As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.Lines
        Get
            Return Array.Empty(Of (String, String, Boolean))
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Dim result As New List(Of String) From
                {
                    NEVER_MIND_TEXT,
                    AVAILABLE_TEXT,
                    ALL_TEXT
                }
            Return result
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return "Verbs"
        End Get
    End Property

    Public Sub New(context As IUIContext, cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
    End Sub

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Select Case choice
            Case ALL_TEXT
                Return New FilteredVerbUIDialog(context, "All Verbs", Function(verbType As IVerbType, character As ICharacter) True, Function() Me)
            Case AVAILABLE_TEXT
                Return New FilteredVerbUIDialog(context, "Available Verbs", Function(verbType As IVerbType, character As ICharacter) verbType.CanPerform(character), Function() Me)
            Case NEVER_MIND_TEXT
                Return cancelDialog()
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
