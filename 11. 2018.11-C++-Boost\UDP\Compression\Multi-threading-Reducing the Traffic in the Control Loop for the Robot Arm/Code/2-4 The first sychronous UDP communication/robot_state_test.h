// Copyright (c) 2017 Franka Emika GmbH
// Use of this source code is governed by the Apache-2.0 license, see LICENSE
#pragma once

#include <array>
#include <ostream>

#include <algorithm>
#include <cstring>
#include <iterator>

//#include <franka/duration.h>
//#include <franka/errors.h>

/**
 * @file robot_state.h
 * Contains the franka::RobotState types.
 */

namespace franka {

/**
 * Describes the robot's current mode.
 */

enum class RobotMode {
  kOther,
  kIdle,
  kMove,
  kGuiding,
  kReflex,
  kUserStopped,
  kAutomaticErrorRecovery
};

/**
 * Describes the robot state.
 */
struct RobotState {
  /**
   * \f$^{O}T_{EE}\f$
   * Measured end effector pose in base frame.
   * Pose is represented as a 4x4 matrix in column-major format.
   */
  std::array<double, 16> O_T_EE{};  // NOLINT(readability-identifier-naming)

  /**
   * \f${^OT_{EE}}_{d}\f$
   * Last desired end effector pose of motion generation in base frame.
   * Pose is represented as a 4x4 matrix in column-major format.
   */
  std::array<double, 16> O_T_EE_d{};  // NOLINT(readability-identifier-naming)

  /**
   * \f$^{F}T_{EE}\f$
   * End effector frame pose in flange frame.
   * Pose is represented as a 4x4 matrix in column-major format.
   */
  std::array<double, 16> F_T_EE{};  // NOLINT(readability-identifier-naming)

  /**
   * \f$^{EE}T_{K}\f$
   * Stiffness frame pose in end effector frame.
   * Pose is represented as a 4x4 matrix in column-major format.
   *
   * See also @ref k-frame "K frame".
   */
  std::array<double, 16> EE_T_K{};  // NOLINT(readability-identifier-naming)

  /**
   * \f$m_{EE}\f$
   * Configured mass of the end effector.
   */
  double m_ee{};

  /**
   * \f$I_{EE}\f$
   * Configured rotational inertia matrix of the end effector load with respect to center of mass.
   */
  std::array<double, 9> I_ee{};  // NOLINT(readability-identifier-naming)

  /**
   * \f$^{F}x_{C_{EE}}\f$
   * Configured center of mass of the end effector load with respect to flange frame.
   */
  std::array<double, 3> F_x_Cee{};  // NOLINT(readability-identifier-naming)

  /**
   * \f$m_{load}\f$
   * Configured mass of the external load.
   */
  double m_load{};

  /**
   * \f$I_{load}\f$
   * Configured rotational inertia matrix of the external load with respect to center of mass.
   */
  std::array<double, 9> I_load{};  // NOLINT(readability-identifier-naming)

  /**
   * \f$^{F}x_{C_{load}}\f$
   * Configured center of mass of the external load with respect to flange frame.
   */
  std::array<double, 3> F_x_Cload{};  // NOLINT(readability-identifier-naming)

  /**
   * \f$m_{total}\f$
   * Sum of the mass of the end effector and the external load.
   */
  double m_total{};

  /**
   * \f$I_{total}\f$
   * Combined rotational inertia matrix of the end effector load and the external load with respect
   * to the center of mass.
   */
  std::array<double, 9> I_total{};  // NOLINT(readability-identifier-naming)

  /**
   * \f$^{F}x_{C_{total}}\f$
   * Combined center of mass of the end effector load and the external load with respect to flange
   * frame.
   */
  std::array<double, 3> F_x_Ctotal{};  // NOLINT(readability-identifier-naming)

  /**
   * Elbow configuration.
   *
   * The values of the array are:
   *  - [0] Position of the 3rd joint in [rad].
   *  - [1] Sign of the 4th joint. Can be +1 or -1.
   */
  std::array<double, 2> elbow{};

  /**
   * Desired elbow configuration.
   *
   * The values of the array are:
   *  - [0] Position of the 3rd joint in [rad].
   *  - [1] Sign of the 4th joint. Can be +1 or -1.
   */
  std::array<double, 2> elbow_d{};

  /**
   * Commanded elbow configuration.
   *
   * The values of the array are:
   *  - [0] Position of the 3rd joint in [rad].
   *  - [1] Sign of the 4th joint. Can be +1 or -1.
   */
  std::array<double, 2> elbow_c{};

  /**
   * Commanded elbow velocity.
   *
   * The values of the array are:
   *  - [0] Velocity of the 3rd joint in [rad/s].
   *  - [1] Sign of the 4th joint. Can be +1 or -1.
   */
  std::array<double, 2> delbow_c{};

  /**
   * Commanded elbow acceleration.
   *
   * The values of the array are:
   *  - [0] Acceleration of the 3rd joint in [rad/s^2].
   *  - [1] Sign of the 4th joint. Can be +1 or -1.
   */
  std::array<double, 2> ddelbow_c{};

  /**
   * \f$\tau_{J}\f$
   * Measured link-side joint torque sensor signals. Unit: \f$[Nm]\f$
   */
  std::array<double, 7> tau_J{};  // NOLINT(readability-identifier-naming)

  /**
   * \f${\tau_J}_d\f$
   * Desired link-side joint torque sensor signals without gravity. Unit: \f$[Nm]\f$
   */
  std::array<double, 7> tau_J_d{};  // NOLINT(readability-identifier-naming)

  /**
   * \f$\dot{\tau_{J}}\f$
   * Derivative of measured link-side joint torque sensor signals. Unit: \f$[\frac{Nm}{s}]\f$
   */
  std::array<double, 7> dtau_J{};  // NOLINT(readability-identifier-naming)

  /**
   * \f$q\f$
   * Measured joint position. Unit: \f$[rad]\f$
   */
  std::array<double, 7> q{};

  /**
   * \f$q_d\f$
   * Desired joint position. Unit: \f$[rad]\f$
   */
  std::array<double, 7> q_d{};

  /**
   * \f$\dot{q}\f$
   * Measured joint velocity. Unit: \f$[\frac{rad}{s}]\f$
   */
  std::array<double, 7> dq{};

  /**
   * \f$\dot{q}_d\f$
   * Desired joint velocity. Unit: \f$[\frac{rad}{s}]\f$
   */
  std::array<double, 7> dq_d{};

  /**
   * \f$\dot{q}_d\f$
   * Desired joint acceleration. Unit: \f$[\frac{rad}{s^2}]\f$
   */
  std::array<double, 7> ddq_d{};

  /**
   * Indicates which contact level is activated in which joint. After contact disappears, value
   * turns to zero.
   *
   * @see Robot::setCollisionBehavior for setting sensitivity values.
   */
  std::array<double, 7> joint_contact{};

  /**
   * Indicates which contact level is activated in which Cartesian dimension (x, y, z, roll, pitch,
   * yaw). After contact disappears, value turns to zero.
   *
   * @see Robot::setCollisionBehavior for setting sensitivity values.
   */
  std::array<double, 6> cartesian_contact{};

  /**
   * Indicates which contact level is activated in which joint. After contact disappears, the value
   * stays the same until a reset command is sent.
   *
   * @see Robot::setCollisionBehavior for setting sensitivity values.
   * @see Robot::automaticErrorRecovery for performing a reset after a collision.
   */
  std::array<double, 7> joint_collision{};

  /**
   * Indicates which contact level is activated in which Cartesian dimension (x, y, z, roll, pitch,
   * yaw). After contact disappears, the value stays the same until a reset command is sent.
   *
   * @see Robot::setCollisionBehavior for setting sensitivity values.
   * @see Robot::automaticErrorRecovery for performing a reset after a collision.
   */
  std::array<double, 6> cartesian_collision{};

  /**
   * \f$\hat{\tau}_{\text{ext}}\f$
   * External torque, filtered. Unit: \f$[Nm]\f$.
   */
  std::array<double, 7> tau_ext_hat_filtered{};

  /**
   * \f$^OF_{K,\text{ext}}\f$
   * Estimated external wrench (force, torque) acting on stiffness frame, expressed
   * relative to the base frame. See also @ref k-frame "K frame".
   * Unit: \f$[N,N,N,Nm,Nm,Nm]\f$.
   */
  std::array<double, 6> O_F_ext_hat_K{};  // NOLINT(readability-identifier-naming)

  /**
   * \f$^{K}F_{K,\text{ext}}\f$
   * Estimated external wrench (force, torque) acting on stiffness frame,
   * expressed relative to the stiffness frame. See also @ref k-frame "K frame".
   * Unit: \f$[N,N,N,Nm,Nm,Nm]\f$.
   */
  std::array<double, 6> K_F_ext_hat_K{};  // NOLINT(readability-identifier-naming)

  /**
   * \f${^OdP_{EE}}_{d}\f$
   * Desired end effector twist in base frame.
   * Unit: \f$[\frac{m}{s},\frac{m}{s},\frac{m}{s},\frac{rad}{s},\frac{rad}{s},\frac{rad}{s}]\f$.
   */
  std::array<double, 6> O_dP_EE_d{};  // NOLINT(readability-identifier-naming)

  /**
   * \f${^OT_{EE}}_{c}\f$
   * Last commanded end effector pose of motion generation in base frame.
   * Pose is represented as a 4x4 matrix in column-major format.
   */
  std::array<double, 16> O_T_EE_c{};  // NOLINT(readability-identifier-naming)

  /**
   * \f${^OdP_{EE}}_{c}\f$
   * Last commanded end effector twist in base frame.
   * Unit: \f$[\frac{m}{s},\frac{m}{s},\frac{m}{s},\frac{rad}{s},\frac{rad}{s},\frac{rad}{s}]\f$.
   */
  std::array<double, 6> O_dP_EE_c{};  // NOLINT(readability-identifier-naming)

  /**
   * \f${^OddP_{EE}}_{c}\f$
   * Last commanded end effector acceleration in base frame.
   * Unit:
   * \f$[\frac{m}{s^2},\frac{m}{s^2},\frac{m}{s^2},\frac{rad}{s^2},\frac{rad}{s^2},\frac{rad}{s^2}]\f$.
   */
  std::array<double, 6> O_ddP_EE_c{};  // NOLINT(readability-identifier-naming)

  /**
   * \f$\theta\f$
   * Motor position. Unit: \f$[rad]\f$
   */
  std::array<double, 7> theta{};

  /**
   * \f$\dot{\theta}\f$
   * Motor velocity. Unit: \f$[rad]\f$
   */
  std::array<double, 7> dtheta{};

  /**
   * Current error state.
   */
  //Errors current_errors{};//++++++++++++++++++++++++++++++++++++++++++++++++++++++++

  /**
   * Contains the errors that aborted the previous motion.
   */
  //Errors last_motion_errors{};//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++

  /**
   * Percentage of the last 100 control commands that were successfully received by the robot.
   *
   * Shows a value of zero if no control or motion generator loop is currently running.
   *
   * Range: \f$[0, 1]\f$.
   */
  double control_command_success_rate{};

  /**
   * Current robot mode.
   */
  RobotMode robot_mode = RobotMode::kUserStopped;

  /**
   * Strictly monotonically increasing timestamp since robot start.
   *
   * Inside of control loops @ref callback-docs "time_step" parameter of Robot::control can be used
   * instead.
   */
  //unsigned long int time{};//Duration time{};+++++++++++++++++++++++++++++++++++++++++++++++
};

/**
 * Streams the robot state as JSON object: {"field_name_1": [0,0,0,0,0,0,0], "field_name_2":
 * [0,0,0,0,0,0], ...}
 */
std::ostream& operator<<(std::ostream& ostream, const franka::RobotState& robot_state);

}  // namespace franka







namespace franka {

namespace {

template <class T, size_t N>
std::ostream& operator<<(std::ostream& ostream, const std::array<T, N>& array) {
  ostream << "[";
  std::copy(array.cbegin(), array.cend() - 1, std::ostream_iterator<T>(ostream, ","));
  std::copy(array.cend() - 1, array.cend(), std::ostream_iterator<T>(ostream));
  ostream << "]";
  return ostream;
}

std::ostream& operator<<(std::ostream& ostream, const RobotMode robot_mode) {
  ostream << "\"";
  switch (robot_mode) {
    case (RobotMode::kUserStopped):
      ostream << "User stopped";
      break;
    case (RobotMode::kIdle):
      ostream << "Idle";
      break;
    case (RobotMode::kMove):
      ostream << "Move";
      break;
    case (RobotMode::kGuiding):
      ostream << "Guiding";
      break;
    case (RobotMode::kReflex):
      ostream << "Reflex";
      break;
    case (RobotMode::kAutomaticErrorRecovery):
      ostream << "Automatic error recovery";
      break;
    case (RobotMode::kOther):
      ostream << "Other";
      break;
  }
  ostream << "\"";
  return ostream;
}

}  // anonymous namespace

std::ostream& operator<<(std::ostream& ostream, const franka::RobotState& robot_state) {
  ostream << "{\"O_T_EE\": " << robot_state.O_T_EE << ", \"O_T_EE_d\": " << robot_state.O_T_EE_d
          << ", \"F_T_EE\": " << robot_state.F_T_EE << ", \"EE_T_K\": " << robot_state.EE_T_K
          << ", \"m_ee\": " << robot_state.m_ee << ", \"F_x_Cee\": " << robot_state.F_x_Cee
          << ", \"I_ee\": " << robot_state.I_ee << ", \"m_load\": " << robot_state.m_load
          << ", \"F_x_Cload\": " << robot_state.F_x_Cload << ", \"I_load\": " << robot_state.I_load
          << ", \"m_total\": " << robot_state.m_total
          << ", \"F_x_Ctotal\": " << robot_state.F_x_Ctotal
          << ", \"I_total\": " << robot_state.I_total << ", \"elbow\": " << robot_state.elbow
          << ", \"elbow_d\": " << robot_state.elbow_d << ", \"tau_J\": " << robot_state.tau_J
          << ", \"tau_J_d\": " << robot_state.tau_J_d << ", \"dtau_J\": " << robot_state.dtau_J
          << ", \"q\": " << robot_state.q << ", \"dq\": " << robot_state.dq
          << ", \"q_d\": " << robot_state.q_d << ", \"dq_d\": " << robot_state.dq_d
          << ", \"joint_contact\": " << robot_state.joint_contact
          << ", \"cartesian_contact\": " << robot_state.cartesian_contact
          << ", \"joint_collision\": " << robot_state.joint_collision
          << ", \"cartesian_collision\": " << robot_state.cartesian_collision
          << ", \"tau_ext_hat_filtered\": " << robot_state.tau_ext_hat_filtered
          << ", \"O_F_ext_hat_K\": " << robot_state.O_F_ext_hat_K
          << ", \"K_F_ext_hat_K\": " << robot_state.K_F_ext_hat_K
          << ", \"O_dP_EE_d\": " << robot_state.O_dP_EE_d << ", \"theta\": " << robot_state.theta
          << ", \"dtheta\": " << robot_state.dtheta
          << ", \"current_errors\": " << /*robot_state.current_errors*/"null"
          << ", \"last_motion_errors\": " << /*robot_state.last_motion_errors*/"null"
          << ", \"control_command_success_rate\": " << robot_state.control_command_success_rate
          << ", \"robot_mode\": " << robot_state.robot_mode
          << ", \"time\": " << /*robot_state.time.toMSec()*/"null" << "}";
  return ostream;
}

}  // namespace franka
