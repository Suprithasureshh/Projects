class SigninForm {
    formvalues={
        username:"",
        password:""
    }
    errorvalues={
        usernameerr:"",
        passworderr:""
    }
    errormsg(index,msg){
        const login=document.getElementsByClassName("login")[index];
        login.classList.add('error');
        login.getElementsByTagName('span')[0].textContent=msg;
    }
    successmsg(index){
        const login=document.getElementsByClassName("login")[index]
        login.classList.remove('error');
        login.classList.add('success')
    }
    getinputs(){
        this.formvalues.username=document.getElementById("uname").value.trim()
        this.formvalues.password=document.getElementById('pswd').value.trim()
    }
    validateUsername(){
        if(this.formvalues.username===""){
            this.errorvalues.usernameerr="Enter Username"
            this.errormsg(0,this.errorvalues.usernameerr)
        }else{
            this.errorvalues.usernameerr=" "
            this.successmsg(0)
        }
    }
    validatePassword(){
        if(this.formvalues.password===""){
            this.errorvalues.passworderr="Enter Password"
            this.errormsg(1,this.errorvalues.passworderr)
        }else if(this.formvalues.password.length<=4){
            this.errorvalues.passworderr="Password must be atleast 6 Characters"
            this.errormsg(1,this.errorvalues.passworderr)
        }else{
            this.errorvalues.passworderr=""
            this.successmsg(1)
        }
    }
}
const UserInputs=new SigninForm();
document.getElementsByClassName("forms")[0].addEventListener('submit',event=>{
    event.preventDefault()
    // console.log(document.getElementsByClassName('form'));
    UserInputs.getinputs()
    UserInputs.validateUsername()
    UserInputs.validatePassword()
})