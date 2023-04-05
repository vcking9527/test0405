import "echarts";
import Vue from "vue";
import veu_echarts from "vue-echarts";
import $api from "./apis/apiClient";
import App from "./App.vue";
import vuetify from "./plugins/vuetify";
import router from "./router";
import store from "./store";

// 將veu_echarts元件設定為全域v-chart元件
Vue.component("v-chart", veu_echarts);

Vue.config.productionTip = false;
Vue.prototype.$api = $api

new Vue({
    router,
    store,
    vuetify,
    render: (h) => h(App),
}).$mount("#app");
