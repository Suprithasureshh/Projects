
function registerform() { 
    
    // validation for image
    var studentimg = document.getElementById("studentimg");
    if(studentimg.value==""){
        document.getElementById("simage").innerHTML="Field should be select";
        studentimg.style.border="1px solid red";
        document.getElementById("simageicon").style.visibility="visible";
        return false;
    }
    else{
        document.getElementById("simage").innerHTML="";
        studentimg.style.border="1px solid black";
        document.getElementById("simageicon").style.visibility="hidden";
        
    }

    // validation for studentname
    var sname = document.getElementById("sname");
    if(sname.value == ""){
        document.getElementById("stdname").innerHTML="Enter name";
        sname.style.border="1px solid red";
        document.getElementById("snameicon").style.visibility="visible";
        return false;
    }
    else{
        document.getElementById("stdname").innerHTML="";
        sname.style.border="1px solid black";
        document.getElementById("snameicon").style.visibility="hidden";
    }

    // validation for fathername
    var fname = document.getElementById("fname");
    if(fname.value == ""){
        document.getElementById("fatname").innerHTML="Enter Fathername";
        fname.style.border="1px solid red";
        document.getElementById("fnameicon").style.visibility="visible";
        return false;
    }
    else{
        document.getElementById("fatname").innerHTML="";
        fname.style.border="1px solid black";
        document.getElementById("fnameicon").style.visibility="hidden";
    }

    // validation for mothername
    var mname = document.getElementById("mname");
    if(mname.value == ""){
        document.getElementById("motname").innerHTML="Enter Mothername";
        mname.style.border="1px solid red";
        document.getElementById("mnameicon").style.visibility="visible";
        return false;
    }
    else{
        document.getElementById("motname").innerHTML="";
        mname.style.border="1px solid black";
        document.getElementById("mnameicon").style.visibility="hidden";
    }

    // validation for radios
    var radios=document.getElementsByName("gender");
    var formvalid=false;
    var i=0;
    if(!formvalid){
        for(i=0;i<radios.length;i++){
            if(radios[i].checked){
                formvalid=true;
                i++;
            }
        }
    }
    if(!formvalid){
         document.getElementById("gen").innerHTML="please select one of the gender";
        
    }
    else{
        document.getElementById("gen").innerHTML=" ";
  
    }
    
    //  validation for date
    var date = document.getElementById("date");
    if(date.value == ""){
        document.getElementById("dateofbirth").innerHTML="Fill the date";
        date.style.border="1px solid red";
     
        return false;
    }
    else{
        document.getElementById("dateofbirth").innerHTML="";
        date.style.border="1px solid black";
       
    }

    // validation for mail
    var mail = document.getElementById("mail");
    if(mail.value == ""){
        document.getElementById("mailto").innerHTML="Enter your mail";
        mail.style.border="1px solid red";
        document.getElementById("mailicon").style.visibility="visible";
        return false;
    }
    else{
        document.getElementById("mailto").innerHTML="";
        mail.style.border="1px solid black";
        document.getElementById("mailicon").style.visibility="hidden";
    }

    // validation for phone
    var phone = document.getElementById("phone");
    if(phone.value == ""){
        document.getElementById("telephone").innerHTML="Number must be Filled out";
        phone.style.border="1px solid red";
        document.getElementById("phoneicon").style.visibility="visible";
        return false;
    }
    else{
        document.getElementById("telephone").innerHTML="";
        phone.style.border="1px solid black";
        document.getElementById("phoneicon").style.visibility="hidden";
    }
    
    //validation for level
    var level = document.getElementById("level");
    if(level.value == ""){
        document.getElementById("course").innerHTML="Field can't be empty";
        level.style.border="1px solid red";
       
        return false;
    }
    else{
        document.getElementById("course").innerHTML="";
        level.style.border="1px solid black";
     
    }
    //check box validation    
    var department =document.getElementsByName("department");
    let error= false;
        if(!error){
            for(var i=0;i<department.length;i++){
                if(department[i].checked){
                    error=true;
               }
            }
        }
    if(!error){
        document.getElementById('checkboxdept').innerHTML="Select any one of the field";
        return false;
    }
    else
    {
        document.getElementById("checkboxdept").innerHTML="";
       
    }
}
