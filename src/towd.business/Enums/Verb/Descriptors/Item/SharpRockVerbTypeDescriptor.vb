﻿Imports towd.data

Friend Class SharpRockVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.SharpRock, business.VerbCategoryType.Craft, 1)
        SetItemTypeInput(business.ItemType.Rock, 1)
        SetItemTypeInput(business.ItemType.Hammer, 1)
        SetItemTypeOutputGenerator(business.ItemType.Hammer, New FixedCharacterWeightedGenerator(1))
        SetItemTypeOutputGenerator(business.ItemType.SharpRock, New FixedCharacterWeightedGenerator(1))
        SetItemTypeInputDurability(business.ItemType.Hammer, 1)
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
        SetFlavorText("Perhaps I played too much OHOL.")
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
    End Sub
End Class
