Namespace TOWD.Business.Tests
    Public Class WorldShould
        <Fact>
        Sub instantiate_a_new_world_with_no_maps()
            Dim subject As IWorld = New World()
            subject.ShouldNotBeNull
            subject.Maps.ShouldBeEmpty
        End Sub
        <Fact>
        Sub create_a_map()
            Dim subject As IWorld = New World
            Dim map As IMap = subject.CreateMap()
            map.ShouldNotBeNull
            subject.Maps.ShouldNotBeEmpty
        End Sub
    End Class
End Namespace

