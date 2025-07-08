Imports towd.data

Friend Class N00bCharacterTypeDescriptor
    Inherits CharacterTypeDescriptor

    Public Sub New()
        MyBase.New(data.CharacterType.N00b, "N00b")
    End Sub

    Public Overrides Sub AdvanceTime(character As ICharacter, amount As Integer)
        If character.GetStatistic(StatisticType.Satiety) > character.GetStatisticMinimum(StatisticType.Satiety) Then
            character.ChangeStatistic(StatisticType.Satiety, -1)
            character.AppendMessage($"-1 Satiety({character.GetStatistic(StatisticType.Satiety)} remaining)")
        Else
            character.ChangeStatistic(StatisticType.Health, -1)
            character.AppendMessage($"Yer starving.")
            character.AppendMessage($"-1 Health({character.GetStatistic(StatisticType.Health)} remaining)")
            If character.IsDead Then
                character.AppendMessage($"Yer dead.")
            End If
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
    End Sub

    Public Overrides Sub Spawn(map As IMap)
        map.World.Avatar = map.World.CreateCharacter(CharacterType.N00b.ToDescriptor, map.GetLocation(map.Columns \ 2, map.Rows \ 2))
    End Sub

    Public Overrides Function GetSpawnCount(map As IMap) As Integer
        Return 1
    End Function
End Class
