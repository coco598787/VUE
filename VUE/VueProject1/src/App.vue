<template>
  <!-- HTML -->
  <img alt="Vue logo" src="./assets/cat.png">
  <HelloWorld msg="Welcome FUEN43"/>
  <DigitalClock displaySeconds="true" blink="true"/>
</template>

<script>  
/* SE5 / SE6 (JS)*/
import HelloWorld from './components/HelloWorld.vue'
import DigitalClock from './components/Clock.vue'

export default {
  name: 'App',
  components: {
    HelloWorld,
    DigitalClock
  }
}
</script>

<style> 
/* 定樣式 */
#app {  
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
