Imports towd.data

Public Interface IItemType
    ReadOnly Property ItemType As ItemType
    ReadOnly Property Name As String
    Sub Initialize(item As IItem)
End Interface
