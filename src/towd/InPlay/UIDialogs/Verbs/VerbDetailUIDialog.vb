Imports towd.business

Public Class VerbDetailUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext
    Private ReadOnly verbType As IVerbType
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Const PERFORM_TEXT = "Perform"

    Sub New(context As IUIContext, verbType As IVerbType, cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.verbType = verbType
        Me.cancelDialog = cancelDialog
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IUIDialog.Lines
        Get
            Return {verbType.Description}
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Dim result As New List(Of String) From {NEVER_MIND_TEXT}
            If verbType.CanPerform(context.World.Avatar) Then
                result.Add(PERFORM_TEXT)
            End If
            Return result
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return verbType.Name
        End Get
    End Property

    Public Function Choose(choice As String) As (String, IUIDialog) Implements IUIDialog.Choose
        Select Case choice
            Case PERFORM_TEXT
                verbType.Perform(context.World.Avatar)
                Return (Nothing, MessageUIDialog.DetermineMessageDialog(context, Function() New VerbDetailUIDialog(context, verbType, cancelDialog)))
            Case NEVER_MIND_TEXT
                Return (Nothing, cancelDialog())
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
