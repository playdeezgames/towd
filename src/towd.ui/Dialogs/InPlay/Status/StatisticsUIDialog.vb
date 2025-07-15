Imports System.Text
Imports towd.business

Friend Class StatisticsUIDialog
    Implements IUIDialog

    Private context As IUIContext
    Private nextDialog As Func(Of IUIDialog)

    Public Sub New(context As IUIContext, nextDialog As Func(Of IUIDialog))
        Me.context = context
        Me.nextDialog = nextDialog
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.Lines
        Get
            Return context.World.Avatar.
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
                                      End Function).Select(Function(x) (Mood.Normal, x, True)).ToList
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Return {"Ok"}
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return "Statistics"
        End Get
    End Property

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Return nextDialog()
    End Function
End Class
