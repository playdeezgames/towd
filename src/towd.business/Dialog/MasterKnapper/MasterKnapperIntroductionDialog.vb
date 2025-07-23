Friend Class MasterKnapperIntroductionDialog
    Implements IDialog

    Private ReadOnly player As ICharacter
    Private ReadOnly knapper As ICharacter
    Private ReadOnly root As IDialog
    Const ACCEPT_TEXT = "I'd like to learn the ways of knapping, and become a knapper like my father."
    Const BYE_TEXT = "..."

    Public Sub New(player As ICharacter, knapper As ICharacter, root As IDialog)
        Me.player = player
        Me.knapper = knapper
        Me.root = root
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IDialog.Lines
        Get
            Return {
                "Hello, the master knapper I am.",
                "No, nothing to do with sleeping this has.",
                "The basics of turning rocks into tools that will allow you to cut I teach."
                }
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IDialog.Choices
        Get
            Return {
                BYE_TEXT,
                ACCEPT_TEXT}
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IDialog.Prompt
        Get
            Return "Master Knapper"
        End Get
    End Property

    Public Function Choose(choice As String) As IDialog Implements IDialog.Choose
        Select Case choice
            Case BYE_TEXT
                Return Nothing
            Case ACCEPT_TEXT
                player.SetTag(MasterKnapperDialog.ACCEPTED_TAG, True)
                Return root
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function
End Class
