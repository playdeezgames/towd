Imports System.Text
Imports towd.business

Friend Class StatisticsUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly nextDialog As Func(Of IUIDialog)

    Public Sub New(context As IUIContext(Of IWorld), nextDialog As Func(Of IUIDialog))
        Me.context = context
        Me.nextDialog = nextDialog
    End Sub

    Public Function GetParametersAsync() As Task(Of IReadOnlyDictionary(Of String, String)) Implements IUIDialog.GetParametersAsync
        Return Task.FromResult(Of IReadOnlyDictionary(Of String, String))(Nothing)
    End Function

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of UIDialogLine)) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of UIDialogLine))(context.World.Avatar.
                EntityType.CharacterType.ToCharacterTypeDescriptor.
                StatisticTypes.Select(Function(x)
                                          Dim statisticType = x.ToStatisticTypeDescriptor
                                          Dim builder As New StringBuilder
                                          builder.Append($"{statisticType.Name}: {context.World.Avatar.GetStatistic(statisticType.StatisticType)}")
                                          Dim maximum = context.World.Avatar.GetStatisticMaximum(statisticType.StatisticType)
                                          If maximum < Integer.MaxValue Then
                                              builder.Append($"/{maximum}")
                                          End If
                                          Return builder.ToString
                                      End Function).Select(Function(x) New UIDialogLine(Mood.Normal, x, True)).ToList)
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Return Task.FromResult(Of IEnumerable(Of String))({"Ok"})
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult("Statistics")
    End Function

    Public Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Return Task.FromResult(nextDialog())
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New StatisticsUIDialog(context, nextDialog))
    End Function
End Class
