Imports towd.business

Public Class VerbDetailUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly verbType As IVerbType
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Private ReadOnly performAgain As Boolean
    Const NEVER_MIND_TEXT = "Never Mind"
    Const PERFORM_TEXT = "Perform"
    Const PERFORM_AGAIN_TEXT = "Perform Again"

    Sub New(
           context As IUIContext(Of IWorld),
           verbType As IVerbType,
           performAgain As Boolean,
           cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.verbType = verbType
        Me.cancelDialog = cancelDialog
        Me.performAgain = performAgain
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean))) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)))({(Mood.Normal, verbType.Description, True)})
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Dim result As New List(Of String)
        If performAgain AndAlso verbType.CanPerform(context.World.Avatar) Then
            result.Add(PERFORM_AGAIN_TEXT)
        End If
        result.Add(NEVER_MIND_TEXT)
        If Not performAgain AndAlso verbType.CanPerform(context.World.Avatar) Then
            result.Add(PERFORM_TEXT)
        End If
        Return Task.FromResult(Of IEnumerable(Of String))(result)
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult(verbType.Name)
    End Function

    Public Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Select Case choice
            Case PERFORM_TEXT, PERFORM_AGAIN_TEXT
                verbType.Perform(context.World.Avatar)
                Return Task.FromResult(MessageUIDialog.DetermineMessageDialog(context, MakeCopy))
            Case NEVER_MIND_TEXT
                Return Task.FromResult(cancelDialog())
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New VerbDetailUIDialog(context, verbType, performAgain, cancelDialog))
    End Function
End Class
