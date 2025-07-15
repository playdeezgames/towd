Imports towd.business

Friend Class NavigationUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext
    Const MOVE_TEXT = "Move..."
    Const INVENTORY_TEXT = "Inventory..."
    Const VERB_TEXT = "Verb..."
    Const DEEDS_TEXT = "Deeds..."
    Const MENU_TEXT = "Game Menu..."
    Const SKILLS_TEXT = "Skills..."
    Const MAP_TEXT = "Map..."
    Const STATISTICS_TEXT = "Statistics..."
    Const DIALOG_TEXT = "Dialog..."

    Public Sub New(context As IUIContext)
        Me.context = context
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IUIDialog.Lines
        Get
            Dim result As New List(Of String)
            Dim character = context.World.Avatar
            Dim location = character.CurrentLocation
            result.Add($"Sat: {character.GetStatistic(StatisticType.Satiety)}/{character.GetStatisticMaximum(StatisticType.Satiety)} | HP: {character.GetStatistic(StatisticType.Health)}/{character.GetStatisticMaximum(StatisticType.Health)} | XP: {character.GetStatistic(StatisticType.XP)}")

            result.Add($"{location}")
            Dim neighbors = location.Neighbors
            For Each neighbor In neighbors

                If character.KnowsLocation(neighbor.Value) Then
                    result.Add($"To the {neighbor.Key.ToDirectionDescriptor.Name} there is {neighbor.Value.EntityType.Name}.")
                Else
                    result.Add($"To the {neighbor.Key.ToDirectionDescriptor.Name} is unexplored.")
                End If
            Next
            If location.HasOtherCharacters(character) Then
                For Each otherCharacter In location.GetOtherCharacters(character)
                    result.Add($"{otherCharacter.Name} is here.")
                Next
            End If
            Return result
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Dim character = context.World.Avatar
            Dim result As New List(Of String) From {
                MOVE_TEXT,
                MAP_TEXT,
                STATISTICS_TEXT
            }
            If Character.CanDialog Then
                result.Add(DIALOG_TEXT)
            End If
            If Character.HasItems Then
                result.Add(INVENTORY_TEXT)
            End If
            If Character.CanDoAnyVerb Then
                result.Add(VERB_TEXT)
            End If
            result.Add(DEEDS_TEXT)
            result.Add(SKILLS_TEXT)
            result.Add(MENU_TEXT)
            Return result
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return "Navigation"
        End Get
    End Property

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Select Case choice
            Case MOVE_TEXT
                Return New MoveMenuUIDialog(context, Function() Me)
            Case MENU_TEXT
                Return New GameMenuUIDialog(context)
            Case VERB_TEXT
                Return New VerbMenuUIDialog(context, Function() Me)
            Case INVENTORY_TEXT
                Return New InventoryUIDialog(context, Function() Me)
            Case DEEDS_TEXT
                Return New DeedsUIDialog(context, Function() Me)
            Case SKILLS_TEXT
                Return New SkillsUIDialog(context, Function() Me)
            Case MAP_TEXT
                Return New MapUIDialog(context, Function() Me)
            Case DIALOG_TEXT
                Return New DialogUIDialog(context, context.World.Avatar.StartDialog(Nothing))
            Case STATISTICS_TEXT
                Return New StatisticsUIDialog(context, Function() Me)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
