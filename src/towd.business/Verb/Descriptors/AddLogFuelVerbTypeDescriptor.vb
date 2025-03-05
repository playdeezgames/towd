Friend Class AddLogFuelVerbTypeDescriptor
    Inherits AddFuelVerbTypeDescriptor
    Public Sub New()
        MyBase.New(
            VerbType.AddLogFuel,
            "Add Log Fuel",
            0,
            2,
            data.ItemType.Log.ToDescriptor,
            {data.LocationType.CookingFire})
    End Sub
End Class
