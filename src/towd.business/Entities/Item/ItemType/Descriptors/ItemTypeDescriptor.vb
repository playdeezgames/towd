﻿Imports towd.data

Public MustInherit Class ItemTypeDescriptor
    Implements IItemType
    Sub New(itemType As ItemType, name As String, isAggregate As Boolean)
        Me.ItemType = itemType
        Me.Name = name
        Me.IsAggregate = isAggregate
    End Sub
    Public ReadOnly Property ItemType As ItemType Implements IItemType.ItemType
    Public ReadOnly Property Name As String Implements IItemType.Name
    Public ReadOnly Property IsAggregate As Boolean Implements IItemType.IsAggregate
    Public MustOverride Sub Initialize(item As IItem) Implements IItemType.Initialize
    Public MustOverride Sub AdvanceTime(item As IItem, amount As Integer) Implements IItemType.AdvanceTime
    Public Overridable Function Describe(item As IItem) As String Implements IItemType.Describe
        Return Name
    End Function
End Class
