Imports towd.business

Friend Class FilteredSkillsUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Private ReadOnly skillFilter As Func(Of ISkillType, Boolean)
    Private ReadOnly table As IReadOnlyDictionary(Of String, ISkillType)
    Const NEVER_MIND_TEXT = "Never Mind"

    Public Sub New(context As IUIContext(Of IWorld), prompt As String, skillFilter As Func(Of ISkillType, Boolean), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me._Prompt = prompt
        Me.cancelDialog = cancelDialog
        Me.skillFilter = skillFilter
        table = SkillTypes.Descriptors.Where(Function(x) skillFilter(x.Value)).ToDictionary(Function(x) $"{x.Value}({x.Value.GetDescription(context.World.Avatar)})", Function(x) x.Value)
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean))) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)))(Array.Empty(Of (String, String, Boolean)))
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Dim result As New List(Of String) From
                {
                    NEVER_MIND_TEXT
                }
        result.AddRange(table.Keys.OrderBy(Function(x) x))
        Return Task.FromResult(Of IEnumerable(Of String))(result)
    End Function

    Private _Prompt As String

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult(_Prompt)
    End Function

    Public Async Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Dim skillType As ISkillType = Nothing
        If table.TryGetValue(choice, skillType) Then
            Dim prompt = Await GetPromptAsync()
            Return New SkillDetailUIDialog(context, context.World.Avatar, skillType, MakeCopy)
        End If
        Return cancelDialog()
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New FilteredSkillsUIDialog(context, _Prompt, skillFilter, cancelDialog))
    End Function
End Class
