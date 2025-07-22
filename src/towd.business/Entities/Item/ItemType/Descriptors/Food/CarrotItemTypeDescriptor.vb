Friend Class CarrotItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.ItemType.Carrot,
            "Carrot",
            True,
            "A knobby, orange root yanked from the unforgiving dirt of TOWD’s wastelands. Crunchy, slightly sweet, and dusted with the grit of a world that doesn’t care if you starve. Eating it raw restores a sliver of hunger but leaves you questioning your life choices. Cook it, craft it, or chuck it at something—freedom’s yours, even if it tastes like despair.",
            statistics:=New Dictionary(Of String, Integer) From
            {
                {StatisticType.Satiety, 10}
            })
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
