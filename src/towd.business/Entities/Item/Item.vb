Imports towd.data

Friend Class Item
    Inherits Entity(Of IItemType, ItemData)
    Implements IItem

    Public Sub New(worldData As data.WorldData, itemId As Integer)
        MyBase.New(worldData, itemId)
    End Sub

    Public Overrides Property EntityType As IItemType
        Get
            Return EntityData.ItemType.ToDescriptor
        End Get
        Set(value As IItemType)
            If EntityData.ItemType <> value.ItemType Then
                EntityData.ItemType = value.ItemType
                EntityData.Statistics.Clear()
                EntityData.Flags.Clear()
                value.Initialize(Me)
            End If
        End Set
    End Property

    Protected Overrides ReadOnly Property EntityData As ItemData
        Get
            Return WorldData.Items(Id)
        End Get
    End Property

    Public Overrides Sub AdvanceTime(amount As Integer)
        EntityType.AdvanceTime(Me, amount)
    End Sub

    Public Sub Recycle() Implements IItem.Recycle
        WorldData.Items(Id) = Nothing
        WorldData.RecycledItems.Add(Id)
    End Sub

    Public Overrides Function ToString() As String
        Return EntityType.Describe(Me)
    End Function
End Class
