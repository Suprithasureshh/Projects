var arrValIndex=0;
let array=[0];


function insert(){

    //for inserting--
    let username = document.getElementById("username").value;
    let mail = document.getElementById("mail").value;
    let phone = document.getElementById("phone").value;
    let level = document.getElementById("level").value;
    let address=document.getElementById("address").value;
    let newRow = '<tr><td>' + username + "</td><td>" + mail + "</td><td>" + phone + "</td><td>" + level + "</td><td>" + address +'</td></tr>';
    document.getElementById('tbody').insertAdjacentHTML("beforeend",newRow);

}
class SignupForm {
    formvalues={
        username:"",
        mail:"",
        phone:"",
        gender:"",
        course:"",
        password:"",
        confirmpassword:"",
        // address:"",
        terms:""
    }
    errorvalues={
        usernameErr:"",
        mailErr:"",
        phoneErr:"",
        genderErr:"",
        courseErr:"",
        passwordErr:"",
        confirmpasswordErr:"",
        // addressErr:"",
        termsErr:""
    }
    ErrorMsg(index,msg){
        const signup_form=document.getElementsByClassName("signup-form")[index];
        signup_form.classList.add('error');
        signup_form.getElementsByTagName('span')[0].textContent=msg;
    }
    SuccessMsg(index){
        const signup_form=document.getElementsByClassName("signup-form")[index]
        signup_form.classList.remove('error');
        signup_form.classList.add('success')
    }
    getInputs(){
        this.formvalues.username=document.getElementById('username').value.trim()
        this.formvalues.mail=document.getElementById('mail').value.trim()
        this.formvalues.phone=document.getElementById('phone').value.trim()
        this.formvalues.gender=document.getElementsByName('gender').value
        this.formvalues.course=document.getElementById('level').value.trim()
        this.formvalues.password=document.getElementById('password').value.trim()
        this.formvalues.confirmpassword=document.getElementById('confirmpassword').value.trim()
        // this.formvalues.address=document.getElementById('address').value.trim()
        this.formvalues.terms=document.getElementsByName('terms1')

    }
    validateUsername(){
        if(this.formvalues.username===""){
            this.errorvalues.usernameErr="Enter Username"
            this.ErrorMsg(0,this.errorvalues.usernameErr)
        }else{
            this.errorvalues.usernameErr=""
            this.SuccessMsg(0)
        }
    }
    validateEmail(){
        var regExp=/^([a-z0-9-_\.]+)@([a-z]+)\.([a-z]{2,10})?$/
        if(this.formvalues.mail===""){
            this.errorvalues.mailErr="Please Enter Valid Email"
            this.ErrorMsg(1,this.errorvalues.mailErr)
        }
        else if(!(regExp.test(this.formvalues.mail))){
            this.errorvalues.mailErr="Invalid Email"
            this.ErrorMsg(1,this.errorvalues.mailErr)
        }
        else{
            this.errorvalues.mailErr=""
            this.SuccessMsg(1)
        }

    }
    validatePhoneNumber(){
        if(this.formvalues.phone===""){
            this.errorvalues.phoneErr="Enter your valid phone number"
            this.ErrorMsg(2,this.errorvalues.phoneErr)
        }
        else if(this.formvalues.phone.length==10){
            this.errorvalues.phoneErr=""
            this.SuccessMsg(2)
        }
        else{
            this.errorvalues.phoneErr="Invalid phone number"
            this.ErrorMsg(2,this.errorvalues.phoneErr)
        }
    }
    validateGender()
    {
    this.formvalues.gender=false;
     var i=0;
     if(!this.formvalues.gender){
        for(i=0;i<gender.length;i++){
            if(gender[i].checked){
                this.formvalues.gender=true;
                i++;
            }
        }
      if(!this.formvalues.gender)
        {
            this.errorvalues.genderErr="Please select gender"
            this.ErrorMsg(3,this.errorvalues.genderErr)
        }else {
            this.errorvalues.genderErr=""
            this.SuccessMsg(3)
        }
        }
    }
    validateCourse(){
        if(this.formvalues.course===""){
            this.errorvalues.courseErr="Select any field"
            this.ErrorMsg(4,this.errorvalues.courseErr)
        }else{
            this.errorvalues.courseErr=""
            this.SuccessMsg(4)
        }
    }
    validatePassword(){
        if(this.formvalues.password===""){
            this.errorvalues.passwordErr="Please Enter Password"
            this.ErrorMsg(5,this.errorvalues.passwordErr)
        }else if(this.formvalues.password.length<=4){
            this.errorvalues.passwordErr="Password must be atleast 6 Characters"
            this.ErrorMsg(5,this.errorvalues.passwordErr)
        }else{
            this.errorvalues.passwordErr=""
            this.SuccessMsg(5)
        }
    }
    validateConfirmPassword(){
        if(this.formvalues.confirmpassword===""){
            this.errorvalues.confirmpasswordErr="Enter Confirm Password"
            this.ErrorMsg(6,this.errorvalues.confirmpasswordErr)
        }
        else if(this.formvalues.confirmpassword==this.formvalues.password){
            this.errorvalues.confirmpasswordErr=""
            this.SuccessMsg(6)
        }
        else{
            this.errorvalues.confirmpasswordErr="password must match"
            this.ErrorMsg(6,this.errorvalues.confirmpasswordErr)
         }
    }
    // validateAddress(){
    //     if(this.formvalues.address===""){
    //         this.errorvalues.addressErr="Fill your Address"
    //         this.ErrorMsg(7,this.errorvalues.addressErr)
    //     }else{
    //         this.errorvalues.addressErr=""
    //         this.SuccessMsg(7)
    //     }
    // }
    validateCheckbox()
    {
        let terms= false;
        if(!terms){
            for(var i=0;i<this.formvalues.terms.length;i++){
                if(this.formvalues.terms[i].checked){
                    terms=true;
               }
            }
        }
        if(!terms){
            this.errorvalues.termsErr="Please confirm the agreement by selecting field"
            this.ErrorMsg(8,this.errorvalues.termsErr)
        }
        else{
            this.errorvalues.termsErr=""
            this.SuccessMsg(8)
        }
    }
    alertMessage(){
        const
        {  
        usernameErr,mailErr,phoneErr,genderErr,courseErr,passwordErr,confirmpasswordErr,termsErr }=this.errorvalues
        if(usernameErr === "" && mailErr === "" && phoneErr === "" && genderErr==="" && courseErr==="" && passwordErr === "" && confirmpasswordErr==="" && termsErr==="") 
        {
            swal("You have successfully registered","Your Input: " + this.formvalues.username,"success").then(()=>{
            this.removeInputs()
            if(this.formvalues.course==="html"){
                window.location.href ="https://www.w3schools.com/html/html_quiz.asp";
            }
            else if(this.formvalues.course==="css"){
                window.location.href ="https://www.w3schools.com/css/css_quiz.asp";
            }
            else{
                window.location.href ="https://www.w3schools.com/js/js_quiz.asp";
            }
            }) 
        } else {
           swal("Something Went Wrong", "Please Check it Once", "error")
        }
    }
    removeInputs(){
        window.location.reload()
    }

}
const UserInputs=new SignupForm();
document.getElementsByClassName("forms")[0].addEventListener('submit',event=>{
    event.preventDefault()
    // console.log(document.getElementsByClassName('form'));
    UserInputs.getInputs()
    UserInputs.validateUsername()
    UserInputs.validateEmail()
    UserInputs.validatePhoneNumber()
    UserInputs.validateGender()
    UserInputs.validateCourse()
    UserInputs.validatePassword()
    UserInputs.validateConfirmPassword()
    // UserInputs.validateAddress()
    UserInputs.validateCheckbox()
    UserInputs.alertMessage()
   
})
