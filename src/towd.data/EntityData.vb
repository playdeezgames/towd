Public Class EntityData
    Public Property Statistics As New Dictionary(Of StatisticType, Integer)
    Public Property StatisticMinimums As New Dictionary(Of StatisticType, Integer)
    Public Property StatisticMaximums As New Dictionary(Of StatisticType, Integer)
    Public Property Flags As New HashSet(Of FlagType)
End Class
