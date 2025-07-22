Imports towd.data

Friend Class TwineVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(business.VerbType.Twine, business.VerbCategoryType.Craft, 1)
        SetItemTypeInput(business.ItemType.PlantFiber, 2)
        SetItemTypeOutputGenerator(business.ItemType.Twine, New FixedGenerator(1))
        SetCharacterStatisticDelta(StatisticType.CraftCounter, 1)
        SetFlavorText("""Fun"" fact: I've actually made twine using milkweed.")
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
    End Sub
End Class
