﻿Imports towd.data

Friend Class N00bCharacterTypeDescriptor
    Inherits CharacterTypeDescriptor

    Public Sub New()
        MyBase.New(business.CharacterType.N00b, "N00b",
                   {
                    StatisticType.Health,
                    StatisticType.Satiety,
                    StatisticType.FoodPoisoning,
                    StatisticType.XP
                   })
    End Sub

    Public Overrides Sub AdvanceTime(character As ICharacter, amount As Integer)
        If character.GetStatistic(StatisticType.FoodPoisoning) > 0 Then
            character.ChangeStatistic(StatisticType.Health, -1)
            character.ChangeStatistic(StatisticType.FoodPoisoning, -1)
            character.AppendMessage($"-1 {StatisticType.FoodPoisoning.ToStatisticTypeDescriptor.Name}({character.GetStatistic(StatisticType.FoodPoisoning)} remaining)")
            character.AppendMessage($"-1 {StatisticType.Health.ToStatisticTypeDescriptor.Name}({character.GetStatistic(StatisticType.Health)} remaining)")
        End If
        If character.GetStatistic(StatisticType.Satiety) > character.GetStatisticMinimum(StatisticType.Satiety) Then
            character.ChangeStatistic(StatisticType.Satiety, -1)
            character.AppendMessage($"-1 {StatisticType.Satiety.ToStatisticTypeDescriptor.Name}({character.GetStatistic(StatisticType.Satiety)} remaining)")
        Else
            character.ChangeStatistic(StatisticType.Health, -1)
            character.AppendMessage($"Yer starving.")
            character.AppendMessage($"-1 {StatisticType.Health.ToStatisticTypeDescriptor.Name}({character.GetStatistic(StatisticType.Health)} remaining)")
        End If
        If character.IsDead Then
            character.AppendMessage($"Yer dead.")
        End If
        For Each descriptor In Deeds.Descriptors.Values
            If Not character.HasDone(descriptor) AndAlso descriptor.HasDone(character) Then
                character.SetDone(descriptor)
            End If
        Next
    End Sub

    Public Overrides Sub Initialize(character As ICharacter)
        character.SetStatisticMaximum(StatisticType.Satiety, 100)
        character.SetStatisticMinimum(StatisticType.Satiety, 0)
        character.SetStatistic(StatisticType.Satiety, 100)

        character.SetStatisticMaximum(StatisticType.Health, 100)
        character.SetStatisticMinimum(StatisticType.Health, 0)
        character.SetStatistic(StatisticType.Health, 100)

        character.SetStatistic(StatisticType.XP, 0)

        character.SetStatistic(StatisticType.ForagingSkill, 1)
        character.SetStatistic(StatisticType.KnappingSkill, 0)
        character.SetStatistic(StatisticType.DigSkill, 1)
        character.SetStatistic(StatisticType.ChopSkill, 1)
        character.SetStatistic(StatisticType.FishSkill, 1)

        character.SetStatistic(StatisticType.Steps, 0)
        character.SetStatistic(StatisticType.ForagingCounter, 0)
        character.SetStatistic(StatisticType.Digging, 0)
        character.SetStatistic(StatisticType.Chopping, 0)
        character.SetStatistic(StatisticType.Fishing, 0)
        character.SetStatistic(StatisticType.CraftCounter, 0)

        character.SetStatistic(StatisticType.FoodPoisoning, 0)
    End Sub

    Public Overrides Sub Spawn(map As IMap)
        Dim candidate As ILocation
        Do
            Dim column = RNG.GenerateInclusiveRange(0, map.Columns - 1)
            Dim row = RNG.GenerateInclusiveRange(0, map.Rows - 1)
            candidate = map.GetLocation(column, row)
        Loop Until Not candidate.Characters.Any
        map.World.Avatar = map.World.CreateCharacter(Me, candidate)
    End Sub

    Public Overrides Function GetSpawnCount(map As IMap) As Integer
        Return 1
    End Function

    Public Overrides Function CanDialog(character As ICharacter) As Boolean
        Return character.CurrentLocation.HasOtherCharacters(character)
    End Function

    Public Overrides Function StartDialog(character As ICharacter, otherCharacter As ICharacter) As IDialog
        If otherCharacter Is Nothing Then
            Return New ChoosePartnerDialog(character)
        Else
            Select Case otherCharacter.EntityType.CharacterType
                Case business.CharacterType.THINDLA
                    Return New THINDLADialog(character)
                Case business.CharacterType.THINDLAsAss
                    Return New THINDLAsAssDialog(character, otherCharacter)
                Case business.CharacterType.MasterKnapper
                    Return New MasterKnapperDialog(character, otherCharacter)
                Case Else
                    Throw New NotImplementedException
            End Select
        End If
    End Function
End Class
