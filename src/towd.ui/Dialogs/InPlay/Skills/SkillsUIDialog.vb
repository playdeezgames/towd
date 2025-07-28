Imports towd.business

Friend Class SkillsUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Const ALL_TEXT = "All"
    Private table As Dictionary(Of String, Func(Of IUIDialog)) = Nothing

    Public Sub New(context As IUIContext(Of IWorld), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of UIDialogLine)) Implements IUIDialog.GetLinesAsync
        Dim result As New List(Of UIDialogLine)
        Dim character = context.World.Avatar
        For Each skillType In SkillTypes.Descriptors
            Dim statisticType = skillType.Value.StatisticType
            If character.HasStatistic(statisticType) Then
                Dim statisticName = statisticType.ToStatisticTypeDescriptor.Name
                result.Add(New UIDialogLine(Mood.Normal, $"Current {statisticName}: {character.GetStatistic(statisticType)}", True))
            End If
        Next
        Return Task.FromResult(Of IEnumerable(Of UIDialogLine))(result)
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Dim result As New List(Of String) From
            {
                NEVER_MIND_TEXT,
                ALL_TEXT
            }
        If table Is Nothing Then
            table = SkillTypes.Descriptors.Values.
                Where(AddressOf FilterSkillType).ToDictionary(Of String, Func(Of IUIDialog))(AddressOf GetTableKey, AddressOf GetTableValue)
        End If
        result.AddRange(table.Keys)
        Return Task.FromResult(Of IEnumerable(Of String))(result)
    End Function

    Private Function GetTableValue(type As ISkillType) As Func(Of IUIDialog)
        Return Function()
                   Return New SkillDetailUIDialog(
                       context,
                       context.World.Avatar,
                       type,
                       MakeCopy)
               End Function
    End Function

    Private Function GetTableKey(type As ISkillType) As String
        Return $"{type}({type.GetDescription(context.World.Avatar)})"
    End Function

    Private Function FilterSkillType(type As ISkillType) As Boolean
        Return ActualSkillTypeFilter(type, context.World.Avatar)
    End Function

    Private Shared Function ActualSkillTypeFilter(type As ISkillType, character As ICharacter) As Boolean
        Return type.CanAdvance(character)
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult("Skills")
    End Function

    Public Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Dim character = context.World.Avatar
        Select Case choice
            Case NEVER_MIND_TEXT
                Return Task.FromResult(cancelDialog())
            Case ALL_TEXT
                Return Task.FromResult(Of IUIDialog)(New FilteredSkillsUIDialog(context, "All Skills", Function(skill) True, MakeCopy))
            Case Else
                Dim nextDialog As Func(Of IUIDialog) = Nothing
                If table.TryGetValue(choice, nextDialog) Then
                    Return Task.FromResult(nextDialog())
                End If
                Throw New NotImplementedException
        End Select
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New SkillsUIDialog(context, cancelDialog))
    End Function
End Class
