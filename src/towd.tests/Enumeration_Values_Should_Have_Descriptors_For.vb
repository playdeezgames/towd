Imports System.Reflection
Imports Shouldly
Imports Xunit

Namespace towd.tests
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
        Sub Topics()
            ValidateEnumModule(Of Topic, ITopic)(AddressOf ToTopicDescriptor)
        End Sub
    End Class
End Namespace

