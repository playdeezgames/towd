Imports towd.business

Friend Class FilteredSkillsUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Private ReadOnly skillFilter As Func(Of ISkillType, Boolean)
    Private ReadOnly table As IReadOnlyDictionary(Of String, ISkillType)
    Const NEVER_MIND_TEXT = "Never Mind"

    Public Sub New(context As IUIContext, prompt As String, skillFilter As Func(Of ISkillType, Boolean), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.prompt = prompt
        Me.cancelDialog = cancelDialog
        Me.skillFilter = skillFilter
        table = SkillTypes.Descriptors.Where(Function(x) skillFilter(x.Value)).ToDictionary(Function(x) $"{x.Value}({x.Value.GetDescription(context.World.Avatar)})", Function(x) x.Value)
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IUIDialog.Lines
        Get
            Return Array.Empty(Of String)
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Dim result As New List(Of String) From
                {
                    NEVER_MIND_TEXT
                }
            result.AddRange(table.Keys.OrderBy(Function(x) x))
            Return result
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt

    Public Function Choose(choice As String) As (String, IUIDialog) Implements IUIDialog.Choose
        Dim skillType As ISkillType = Nothing
        If table.TryGetValue(choice, skillType) Then
            Return (Nothing, New SkillDetailUIDialog(context.World.Avatar, skillType, Function() New FilteredSkillsUIDialog(context, Prompt, skillFilter, cancelDialog)))
        End If
        Return (Nothing, cancelDialog())
    End Function
End Class
