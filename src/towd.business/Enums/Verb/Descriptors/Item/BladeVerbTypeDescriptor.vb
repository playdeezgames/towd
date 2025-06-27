Imports towd.data

Friend Class BladeVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Blade, 1)
        SetItemTypeInput(ItemType.SharpRock, 1)
        SetItemTypeInput(ItemType.Hammer, 1)
        SetItemTypeInputDurability(ItemType.Hammer, 1)
        SetItemTypeOutputGenerator(ItemType.Hammer, New FixedGenerator(1))
        SetItemTypeOutputGenerator(ItemType.Blade, New FixedGenerator(1))
        SetCharacterStatisticMinimum(StatisticType.KnappingSkill, 1)
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
    End Sub
End Class
