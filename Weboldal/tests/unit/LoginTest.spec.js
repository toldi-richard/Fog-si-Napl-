import { mount } from '@vue/test-utils'
import Login from '@/views/Login.vue'

describe('Login tesztelése',  () => {
    test('Email mező kitöltésének tesztje', async () => {
        const wrapper = mount(Login)
        const input = wrapper.find('input[type="email"]') 
        await input.setValue('admin@gmail.com')
        expect(wrapper.vm.login_form.email).toBe('admin@gmail.com')
    })
    test('Jelszó mező kitöltésének tesztje', async () => {
        const wrapper = mount(Login)
        const input = wrapper.find('input[type="password"]') 
        await input.setValue('admin1987')
        expect(wrapper.vm.login_form.password).toBe('admin1987')
    })
})
