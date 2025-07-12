Imports System.Reflection
Imports Shouldly
Imports towd.business
Imports towd.data
Imports Xunit

Public Class Topics_Should_Exist_For
    Private Shared Sub ValidateEnumModule(Of TEnum)(toTopic As Func(Of String, String))
        Dim enumTypeModule = GetType(TEnum)
        Dim enumTypes = enumTypeModule.GetFields(BindingFlags.Public Or BindingFlags.Static)
        For Each enumType In enumTypes
            Dim enumTypeValue = CStr(enumType.GetRawConstantValue())
            Dim topic = toTopic(enumTypeValue)
            topic.ToTopicDescriptor.ShouldNotBeNull($"Topic {topic} should have a descriptor!")
        Next
    End Sub
    <Fact>
    Public Sub Item_Types()
        ValidateEnumModule(Of ItemType)(AddressOf Topics.ToItemTypeTopic)
    End Sub
    <Fact>
    Public Sub Verb_Types()
        ValidateEnumModule(Of VerbType)(AddressOf Topics.ToVerbTypeTopic)
    End Sub
End Class
