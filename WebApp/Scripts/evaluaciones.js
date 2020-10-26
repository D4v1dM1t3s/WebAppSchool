var evaluacionesAdm = {
    profesorCod: null,
    alumnoCod: null,
    //To get selected value an text of dropdownlist
    init: function SelectedValueProfesor(ddlObject) {
        //Selected value of dropdownlist
        var selectedValue = ddlObject.value;
        //Selected text of dropdownlist
        var selectedText = ddlObject.options[ddlObject.selectedIndex].innerHTML;

        profesorCod = selectedValue;
        //alert popup with detail of seleceted value and text
        alert(" Selected Value: " + selectedValue + " -- " + "Selected Text: " + selectedText);
    },

    init: function SelectedValueAlumno(ddlObject) {
        //Selected value of dropdownlist
        var selectedValue = ddlObject.value;
        //Selected text of dropdownlist
        var selectedText = ddlObject.options[ddlObject.selectedIndex].innerHTML;

        alumnoCod = selectedValue;
        //alert popup with detail of seleceted value and text
        alert(" Selected Value: " + selectedValue + " -- " + "Selected Text: " + selectedText);
    },

    $("#btnSearch").click(function () {
        alert($("#alumnoCod").val());
    });
};


evaluacionesAdm.init();