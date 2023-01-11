<script setup></script>

<template>
  <!-- Hero Content -->
  <div class="bg-primary-dark">
    <div class="content content-full text-center pt-7 pb-5">
      <h1 class="h2 text-white mb-2">Contact Us.</h1>
      <h2 class="h4 fw-normal text-white-75">
        Do you need any assistance with your plan? Let us know!
      </h2>
    </div>
  </div>
  <!-- END Hero Content -->

  <!-- Page Content -->
  <div class="bg-body-extra-light">
    <div class="content">
      <div class="row items-push justify-content-center">
        <div class="col-md-10 col-xl-5">
          <form @submit.prevent>
            <div class="row">
              <div class="col-md-6">
                <div class="mb-4">
                  <label class="form-label" for="firstname">Firstname</label>
                  <input
                    type="text"
                    class="form-control"
                    id="firstname"
                    name="firstname"
                    placeholder="Enter your firstname.."
                  />
                </div>
              </div>
              <div class="col-md-6">
                <div class="mb-4">
                  <label class="form-label" for="lastname">Lastname</label>
                  <input
                    type="text"
                    class="form-control"
                    id="lastname"
                    name="lastname"
                    placeholder="Enter your lastname.."
                  />
                </div>
              </div>
            </div>
            <div class="mb-4">
              <label class="form-label" for="email">Email</label>
              <input
                type="email"
                class="form-control"
                id="email"
                name="email"
                placeholder="Enter your email.."
              />
            </div>
            <div class="mb-4">
              <label class="form-label" for="subject">Custom Menu</label>
              <select class="form-select" id="subject" name="subject" size="1">
                <option value="1">Support</option>
                <option value="2">Billing</option>
                <option value="3">Management</option>
                <option value="4">Feature Request</option>
              </select>
            </div>
            <div class="mb-4">
              <label class="form-label" for="msg">Message</label>
              <textarea
                class="form-control"
                id="msg"
                name="msg"
                rows="7"
                placeholder="Enter your message.."
              ></textarea>
            </div>
            <div class="mb-4">
              <button type="submit" class="btn btn-alt-primary">
                <i class="fa fa-paper-plane me-1 opacity-50"></i> Send Message
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
  <!-- END Page Content -->
</template>
