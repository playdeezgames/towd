﻿Imports towd.business

Friend Class NavigationUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Const MOVE_TEXT = "Move..."
    Const INVENTORY_TEXT = "Inventory..."
    Const VERB_TEXT = "Verb..."
    Const DEEDS_TEXT = "Deeds..."
    Const MENU_TEXT = "Game Menu..."
    Const SKILLS_TEXT = "Skills..."
    Const MAP_TEXT = "Map..."
    Const STATISTICS_TEXT = "Statistics..."
    Const DIALOG_TEXT = "Dialog..."
    Const LOCATION_TEXT = "Location..."
    Const CHARACTER_TEXT = "Character..."

    Public Sub New(context As IUIContext(Of IWorld))
        Me.context = context
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of UIDialogLine)) Implements IUIDialog.GetLinesAsync
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
        Return Task.FromResult(Of IEnumerable(Of UIDialogLine))(result.Select(Function(x) New UIDialogLine(Mood.Normal, x, True)))
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Dim character = context.World.Avatar
        Dim result As New List(Of String) From {
            MOVE_TEXT,
            MAP_TEXT,
            STATISTICS_TEXT,
            LOCATION_TEXT
        }
        If character.CanDialog Then
            result.Add(DIALOG_TEXT)
        End If
        If character.HasItems Then
            result.Add(INVENTORY_TEXT)
        End If
        If character.CanDoAnyVerb Then
            result.Add(VERB_TEXT)
        End If
        result.Add(DEEDS_TEXT)
        result.Add(SKILLS_TEXT)
        result.Add(MENU_TEXT)
        Return Task.FromResult(Of IEnumerable(Of String))(result)
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult("Navigation")
    End Function

    Public Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Select Case choice
            Case MOVE_TEXT
                Return Task.FromResult(Of IUIDialog)(New MoveMenuUIDialog(context, MakeCopy))
            Case MENU_TEXT
                Return Task.FromResult(Of IUIDialog)(New GameMenuUIDialog(context))
            Case VERB_TEXT
                Return Task.FromResult(Of IUIDialog)(New VerbMenuUIDialog(context, MakeCopy))
            Case INVENTORY_TEXT
                Return Task.FromResult(Of IUIDialog)(New InventoryUIDialog(context, MakeCopy))
            Case DEEDS_TEXT
                Return Task.FromResult(Of IUIDialog)(New DeedsUIDialog(context, MakeCopy))
            Case SKILLS_TEXT
                Return Task.FromResult(Of IUIDialog)(New SkillsUIDialog(context, MakeCopy))
            Case MAP_TEXT
                Return Task.FromResult(Of IUIDialog)(New MapUIDialog(context, MakeCopy))
            Case DIALOG_TEXT
                Return Task.FromResult(Of IUIDialog)(New DialogUIDialog(context, context.World.Avatar.StartDialog(Nothing)))
            Case STATISTICS_TEXT
                Return Task.FromResult(Of IUIDialog)(New StatisticsUIDialog(context, MakeCopy))
            Case LOCATION_TEXT
                Return Task.FromResult(Of IUIDialog)(New LocationUIDialog(context, MakeCopy))
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New NavigationUIDialog(context))
    End Function
End Class
