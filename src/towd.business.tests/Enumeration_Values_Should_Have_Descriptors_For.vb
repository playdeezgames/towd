Imports System.Reflection
Imports Shouldly
Imports towd.data
Imports Xunit

Namespace towd.business.tests
    Public Class Enumeration_Values_Should_Have_Descriptors_For
        Private Shared Sub ValidateEnumModule(Of TEnum, TDescriptor As Class)(toDescriptor As Func(Of String, TDescriptor))
            Dim enumTypeModule = GetType(TEnum)
            Dim enumTypes = enumTypeModule.GetFields(BindingFlags.Public Or BindingFlags.Static)
            For Each enumType In enumTypes
                Dim enumTypeValue = CStr(enumType.GetRawConstantValue())
                toDescriptor(enumTypeValue).ShouldNotBeNull($"{enumTypeModule.Name}.{enumType.Name} should have a descriptor!")
            Next
        End Sub
        <Fact>
        Sub Verb_Category_Types()
            ValidateEnumModule(Of VerbCategoryType, IVerbCategoryType)(AddressOf ToVerbCategoryDescriptor)
        End Sub
        <Fact>
        Sub Character_Types()
            ValidateEnumModule(Of CharacterType, ICharacterType)(AddressOf ToCharacterTypeDescriptor)
        End Sub
        <Fact>
        Sub Item_Types()
            ValidateEnumModule(Of ItemType, IItemType)(AddressOf ToItemTypeDescriptor)
        End Sub
        <Fact>
        Sub Map_Types()
            ValidateEnumModule(Of MapType, IMapType)(AddressOf ToMapTypeDescriptor)
        End Sub
        <Fact>
        Sub Location_Types()
            ValidateEnumModule(Of LocationType, ILocationType)(AddressOf ToLocationTypeDescriptor)
        End Sub
        <Fact>
        Sub Verb_Types()
            ValidateEnumModule(Of VerbType, IVerbType)(AddressOf ToVerbTypeDescriptor)
        End Sub
    End Class
End Namespace

