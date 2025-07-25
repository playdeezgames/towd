Imports towd.business

Friend Class FilteredDeedsUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Private ReadOnly table As IReadOnlyDictionary(Of String, IDeed)
    Const NEVER_MIND_TEXT = "Never Mind"

    Public Sub New(context As IUIContext(Of IWorld), prompt As String, deedFilter As Func(Of IDeed, Boolean), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
        Me._Prompt = prompt
        Me.deedFilter = deedFilter
        table = Deeds.Descriptors.Where(Function(x) deedFilter(x.Value)).ToDictionary(Function(x) x.Value.Name, Function(x) x.Value)
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean))) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)))(Array.Empty(Of (String, String, Boolean)))
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Dim result As New List(Of String) From {
                    NEVER_MIND_TEXT
                }
        result.AddRange(table.Keys.OrderBy(Function(x) x))
        Return Task.FromResult(Of IEnumerable(Of String))(result)
    End Function

    Private _Prompt As String

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult(_Prompt)
    End Function

    Private ReadOnly deedFilter As Func(Of IDeed, Boolean)

    Public Async Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Dim deed As IDeed = Nothing
        If table.TryGetValue(choice, deed) Then
            Dim prompt = Await GetPromptAsync()
            Return New DeedDetailUIDialog(deed, MakeCopy)
        End If
        Return cancelDialog()
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New FilteredDeedsUIDialog(context, _Prompt, deedFilter, cancelDialog))
    End Function
End Class
