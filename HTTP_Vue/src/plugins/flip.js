
window.addEventListener("deviceorientation", handleOrientation);

function handleOrientation(event){
  console.log(event.absolute);
  if (event.beta > 100 || event.beta < -100){
    window.location.href = 'pause.html';
  }
}
